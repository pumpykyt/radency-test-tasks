using RadencyTestTasks.Task1.Requests;
using RadencyTestTasks.Task1.Responses;

namespace RadencyTestTasks.Task1.Domain.Models;

public abstract class FileDetails
{
    protected string FileName { get; }
    protected string FullFilePath { get; }

    protected FileDetails(string fileName, string fullFilePath)
    {
        FileName = fileName;
        FullFilePath = fullFilePath;
    }

    public abstract Task<FileContentResponse> ReadAsync();
}