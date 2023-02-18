using Newtonsoft.Json;
using RadencyTestTasks.Task1.Responses;

namespace RadencyTestTasks.Task1.Helpers;

public static class FileHelper
{
    public static List<string> GetAllFilePathsFromDirectory(string directoryPath) 
        => Directory.GetFiles(directoryPath, "*.txt", SearchOption.TopDirectoryOnly)
                    .Concat(Directory.GetFiles(directoryPath, "*.csv", SearchOption.TopDirectoryOnly))
                    .ToList();

    public static async Task SaveFileResultAsync(List<PaymentTransactionResponse> data, string directory)
    {
        var subDirName = DateTime.UtcNow.ToString("MM-dd-yyyy");
        var newDirPath = Path.Combine(directory, subDirName);
        if (!Directory.Exists(newDirPath)) Directory.CreateDirectory(newDirPath);
        var json = JsonConvert.SerializeObject(data);
        var fileName = $"output{Directory.GetFiles(newDirPath).Length + 1}.json";
        await File.WriteAllTextAsync(Path.Combine(newDirPath, fileName), json);
    }
}