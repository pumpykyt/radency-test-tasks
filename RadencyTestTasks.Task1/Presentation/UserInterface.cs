using RadencyTestTasks.Task1.Domain;
using RadencyTestTasks.Task1.Domain.Interfaces;

namespace RadencyTestTasks.Task1.Presentation;

public static class UserInterface
{
    private static IApplication _application = new Application();
    
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
                    var task = Task.Run(async () => await _application.StartAsync());
                    break;
                case 2:
                    Environment.Exit(1);
                    break;
            }
        }
    }
}