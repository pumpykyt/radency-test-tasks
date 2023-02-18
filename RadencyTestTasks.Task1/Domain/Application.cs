using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using RadencyTestTasks.Task1.Domain.Factories;
using RadencyTestTasks.Task1.Domain.Interfaces;
using RadencyTestTasks.Task1.Domain.Models;
using RadencyTestTasks.Task1.Domain.Services;
using RadencyTestTasks.Task1.Global;
using RadencyTestTasks.Task1.Helpers;
using RadencyTestTasks.Task1.Jobs;
using RadencyTestTasks.Task1.Responses;
using RadencyTestTasks.Task1.Settings;

namespace RadencyTestTasks.Task1.Domain;

public class Application : IApplication
{
    private readonly FileSystemWatcher _fileSystemWatcher;
    private readonly ServiceProvider _serviceProvider;
    private readonly ConcurrentBag<FileContentResponse> _entries;
    private readonly StdSchedulerFactory _quartzFactory;
    private readonly IScheduler _quartzScheduler;
    private readonly IFileSaveService _fileSaveService;
    private readonly IFactory _csvFactory;
    private readonly IFactory _txtFactory;
    private readonly FileSettings _fileSettings;

    public Application()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName)
            .AddJsonFile(GlobalVariables.SettingFileName)
            .Build();
        
        _serviceProvider = new ServiceCollection()
            .AddScoped<IConfiguration>(cfg => configuration)
            .AddSingleton<IFactory, CsvFactory>()
            .AddSingleton<IFileSaveService, FileSaveService>()
            .AddSingleton<IFactory, TxtFactory>()
            .BuildServiceProvider();
        
        
        _quartzFactory = new StdSchedulerFactory();
        _quartzScheduler = _quartzFactory.GetScheduler().Result;
        _entries = new ConcurrentBag<FileContentResponse>();
        _fileSaveService = _serviceProvider.GetService<IFileSaveService>();
        _fileSettings = _serviceProvider.GetService<IConfiguration>().Get<FileSettings>();
        _csvFactory = _serviceProvider.GetServices<IFactory>()
                                     .First(t => t.GetType() == typeof(CsvFactory));
        _txtFactory = _serviceProvider.GetServices<IFactory>()
                                     .First(t => t.GetType() == typeof(TxtFactory));
        _fileSystemWatcher = new FileSystemWatcher(_fileSettings.SourceDirectory);
    }
    
    private async Task FileCreated(object s, FileSystemEventArgs e)
    {
        Thread.Sleep(500);
        await ReadSingleFileAsync(e.FullPath);
    } 

    private void ConfigureWatcher()
    {
        _fileSystemWatcher.EnableRaisingEvents = true;
        _fileSystemWatcher.Filters.Add("*.txt");
        _fileSystemWatcher.Filters.Add("*.csv");
        _fileSystemWatcher.Created += async (s, e) => await FileCreated(s, e);
    }

    private async Task ReadSingleFileAsync(string filePath)
    {
        var fileName = Path.GetFileName(filePath);
        var fileExtension = Path.GetExtension(fileName);
        switch (fileExtension)
        {
            case ".txt":
                var txtFileDetails = _txtFactory.CreateFileDetails(fileName, filePath);
                _entries.Add(await txtFileDetails.ReadAsync());
                break;
            case ".csv":
                var csvFileDetails = _txtFactory.CreateFileDetails(fileName, filePath);
                _entries.Add(await csvFileDetails.ReadAsync());
                break;
        }

        var result = _entries.Last().MapToPaymentTransactionResponse();
        await _fileSaveService.SaveFileResultAsync(result, _fileSettings.OutputDirectory);
    }

    private void ReadAllFiles()
    {
        var files = Directory
            .GetFiles(_fileSettings.SourceDirectory, "*.txt", SearchOption.TopDirectoryOnly)
            .Concat(Directory.GetFiles(_fileSettings.SourceDirectory, "*.csv", SearchOption.TopDirectoryOnly))
            .ToList();
        
        int fileCounter = 0;
        
        Parallel.ForEach(files, file =>
        {
            var fileExtension = Path.GetExtension(file);
            switch (fileExtension)
            {
                case ".txt":
                    var txtFileDetails = _txtFactory.CreateFileDetails(Path.GetFileName(file), file);
                    _entries.Add(txtFileDetails.ReadAsync().Result);
                    break;
                case ".csv":
                    var csvFileDetails = _csvFactory.CreateFileDetails(file, file);
                    _entries.Add(csvFileDetails.ReadAsync().Result);
                    break;
            }  
            Interlocked.Increment(ref fileCounter);
        });
    }

    private bool SettingsFileExists()
    {
        var settingsDirectoryPath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        var settingsPath = Path.Combine(settingsDirectoryPath, GlobalVariables.SettingFileName);
        return File.Exists(settingsPath);
    }

    private async Task ConfigureQuartzAsync()
    {
        await _quartzScheduler.Start();

        var job = JobBuilder.Create<MidnightJob>()
            .WithIdentity("j1", "g1")
            .UsingJobData("Directory", _fileSettings.OutputDirectory)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("t1", "g1")
            .WithCronSchedule("0 0 0 * * ?")
            .Build();

        await _quartzScheduler.ScheduleJob(job, trigger);
    }

    public async Task StartAsync()
    {
        if (!SettingsFileExists())
        {
            Console.WriteLine("Settings file 'appsettings.json' not exists! Please create it and configure");
            Environment.Exit(-1);
        }

        ReadAllFiles();
        ConfigureWatcher();
        await ConfigureQuartzAsync();
        while (true)
        {
            
        }
    }
}