using RadencyTestTasks.Task1.Responses;

namespace RadencyTestTasks.Task1.Interfaces;

public interface IStrategy
{
    Task<FileContentResponse> ReadDataAsync(string filename);
}