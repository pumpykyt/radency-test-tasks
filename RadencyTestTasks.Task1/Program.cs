using RadencyTestTasks.Task1.Contexts;
using RadencyTestTasks.Task1.Helpers;
using RadencyTestTasks.Task1.Interfaces;

Console.WriteLine(1);

string data = "John, Doe, “Lviv, Kleparivska 35, 4”, 500.0, 2022-27-01, 1234567, Water\n" +
              "Luke, Pan, “Lviv, Gorodotska 120, 5”, 40.0, 2022-12-07, 2222111, Gas\n" +
              "Luke, Pan, “Lviv, Gorodotska 120, 5”, 40.0, 2022-12-07, 2222111, Gas\n" +
              "Luke Pan,, “Lviv, Gorodotska 120, 5”, 40.0, 2022-12-07, 2222111, Gas\n" +
              "Luke Pan,, “Lviv, Gorodotska 120, 5”, 40.0, 2022-12-07, 2222111, Gas";

IFileContext context = new FileContext();
context.Test(data);