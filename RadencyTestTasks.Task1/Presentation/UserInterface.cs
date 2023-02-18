using RadencyTestTasks.Task1.Domain;
using RadencyTestTasks.Task1.Domain.Interfaces;
using RadencyTestTasks.Task1.Global;

namespace RadencyTestTasks.Task1.Presentation;

public static class UserInterface
{
    private static readonly IApplication Application = new Application();
    
    public static async Task Menu()
    {
        while (true)
        {
            Console.WriteLine("[1] - START\t[2] - STOP");
            string userInput = Console.ReadLine();
            if (!(int.TryParse(userInput, out int numericInput)))
            {
                Console.WriteLine("Only numbers allowed. Press 'q' to exit.");
                continue;
            }

            switch (numericInput)
            {
                case 1:
                    await Task.Run(async () => await Application.StartAsync());
                    break;
                case 2:
                    Console.WriteLine(GlobalVariables.ToString());
                    Thread.Sleep(50000);
                    Environment.Exit(1);
                    break;
            }
        }
    }
}