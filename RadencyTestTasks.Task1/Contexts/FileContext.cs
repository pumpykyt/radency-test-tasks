using System.Collections.Concurrent;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using RadencyTestTasks.Task1.Helpers;
using RadencyTestTasks.Task1.Interfaces;
using RadencyTestTasks.Task1.Requests;
using RadencyTestTasks.Task1.Responses;
using RadencyTestTasks.Task1.Settings;
using RadencyTestTasks.Task1.Strategies;

namespace RadencyTestTasks.Task1.Contexts;

public class FileContext
{
    private IStrategy _strategy;
    private readonly ConcurrentBag<FileContentResponse> _entries;
    private FileSettings _fileSettings;

    public FileContext()
    {
        InitFileSettings();
        _strategy = new TxtStrategy();
        _entries = new ConcurrentBag<FileContentResponse>();
    }
    public FileContext(IStrategy strategy) => _strategy = strategy;
    private void SetStrategy(IStrategy strategy) => _strategy = strategy;
    private void InitFileSettings()
    {
        var settingsPath = Path.Combine(
            Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName, 
            "appsettings.json"
        );
        var settings = File.ReadAllText(settingsPath);
        _fileSettings = JsonConvert.DeserializeObject<FileSettings>(settings);
    }

    public async Task ReadSingleFile(string fileName)
    {
        var fileExtension = Path.GetExtension(fileName);
        var filePath = Path.Combine(_fileSettings.SourceDirectory, fileName);
        switch (fileExtension)
        {
            case ".txt":
                SetStrategy(new TxtStrategy());
                _entries.Add(await _strategy.ReadDataAsync(filePath));
                break;
            case ".csv":
                SetStrategy(new CsvStrategy());
                _entries.Add(await _strategy.ReadDataAsync(filePath));
                break;
        }

        var result = _entries.First().MapToPaymentTransactionResponse();
        await FileHelper.SaveFileResultAsync(result, _fileSettings.OutputDirectory);
        Console.WriteLine();
    }

    public void ReadAllFiles()
    {
        var files = FileHelper.GetAllFilePathsFromDirectory(_fileSettings.SourceDirectory);
        int fileCounter = 0;
        
        Parallel.ForEach(files, file =>
        {
            var fileExtension = Path.GetExtension(file);
            switch (fileExtension)
            {
                case ".txt":
                    SetStrategy(new TxtStrategy());
                    _entries.Add(_strategy.ReadDataAsync(file).Result);
                    break;
                case ".csv":
                    SetStrategy(new CsvStrategy());
                    _entries.Add(_strategy.ReadDataAsync(file).Result);
                    break;
            }  
            Interlocked.Increment(ref fileCounter);
        });
    }
}