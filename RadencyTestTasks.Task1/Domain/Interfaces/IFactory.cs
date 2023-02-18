using RadencyTestTasks.Task1.Domain.Models;

namespace RadencyTestTasks.Task1.Domain.Interfaces;

public interface IFactory
{
    FileDetails CreateFileDetails(string fileName, string fullFilePath);
}