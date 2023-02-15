using System.Text.RegularExpressions;
using RadencyTestTasks.Task1.Helpers;
using RadencyTestTasks.Task1.Interfaces;
using RadencyTestTasks.Task1.Requests;
using RadencyTestTasks.Task1.Responses;

namespace RadencyTestTasks.Task1.Contexts;

public class FileContext : IFileContext
{
    public Task<List<PaymentTransactionRequest>> ReadTransactionsFromFileAsync(string fileName)
    {
        throw new NotImplementedException();
    }

    public void Test(string data)
    {
        var validTransactions = new List<PaymentTransactionRequest>();
        int invalidLinesCount = 0;
        var regex = new Regex(
            "[A-Za-z]+, [A-Za-z]+, “[A-Za-z]+,\\s[A-Za-z]+ [0-9]+, [0-9]+”, " +
            "([+-]?(?=\\.\\d|\\d)(?:\\d+)?(?:\\.?\\d*))(?:[Ee]([+-]?\\d+))?, " +
            "[0-9]{4}-[0-9]{2}-[0-9]{2}, [0-9]+, [A-Za-z]+", 
            RegexOptions.IgnoreCase
        );

        var rows = data.Split('\n');

        foreach (var row in rows)
        {
            if (ParseHelper.ValidateRow(row, regex))
            {
                var transaction = ParseHelper.ParsePaymentTransaction(row);
                validTransactions.Add(transaction);
            }
            else
            {
                invalidLinesCount++;
            }
        }

        Console.WriteLine($"Invalid lines count: {invalidLinesCount}");
    }
}