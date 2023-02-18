using RadencyTestTasks.Task1.Requests;
using RadencyTestTasks.Task1.Responses;

namespace RadencyTestTasks.Task1.Domain.Models;

public abstract class FileDetails
{
    public string FileName { get; set; }
    public string FullFilePath { get; set; }

    protected FileDetails(string fileName, string fullFilePath)
    {
        FileName = fileName;
        FullFilePath = fullFilePath;
    }

    public abstract Task<FileContentResponse> ReadAsync();
}