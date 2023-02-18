using RadencyTestTasks.Task1.Domain.Interfaces;
using RadencyTestTasks.Task1.Domain.Models;

namespace RadencyTestTasks.Task1.Domain.Factories;

public class CsvFactory : IFactory
{
    public FileDetails CreateFileDetails(string fileName, string fullFilePath) 
        => new CsvFileDetails(fileName, fullFilePath);
}