using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RadencyTestTasks.Task1.Domain.Interfaces;
using RadencyTestTasks.Task1.Global;
using RadencyTestTasks.Task1.Responses;

namespace RadencyTestTasks.Task1.Domain.Services;

public class FileSaveService : IFileSaveService
{
    private readonly IConfiguration _configuration;

    public FileSaveService(IConfiguration configuration) => _configuration = configuration;

    public async Task SaveFileResultAsync(List<PaymentTransactionResponse> data, string directory)
    {
        var subDirName = DateTime.UtcNow.ToString("MM-dd-yyyy");
        var newDirPath = Path.Combine(directory, subDirName);
        if (!Directory.Exists(newDirPath)) Directory.CreateDirectory(newDirPath);
        var json = JsonConvert.SerializeObject(data);
        var fileName = $"output{Directory.GetFiles(newDirPath).Length + 1}.json";
        await File.WriteAllTextAsync(Path.Combine(newDirPath, fileName), json);
    }
}