using RadencyTestTasks.Task1;
using RadencyTestTasks.Task1.Contexts;

var fileContext = new FileContext();
await fileContext.ReadSingleFile("test.txt");
Console.WriteLine();
