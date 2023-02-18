using System.Text.RegularExpressions;
using RadencyTestTasks.Task1.Helpers;
using RadencyTestTasks.Task1.Interfaces;
using RadencyTestTasks.Task1.Requests;
using RadencyTestTasks.Task1.Responses;

namespace RadencyTestTasks.Task1.Strategies;

public class TxtStrategy : IStrategy
{
    public async Task<FileContentResponse> ReadDataAsync(string filename)
    {
        if (!File.Exists(filename)) throw new FileNotFoundException();

        var data = await File.ReadAllLinesAsync(filename);
        var validTransactions = new List<PaymentTransactionRequest>();
        int invalidLinesCount = 0;

        foreach (var row in data)
        {
            var isValid = ParseHelper.ValidateRow(row);
            if (isValid)
            {
                var transaction = ParseHelper.ParsePaymentTransaction(row);
                validTransactions.Add(transaction);
            }
            else
            {
                invalidLinesCount++;
            }
        }

        return new FileContentResponse(validTransactions, invalidLinesCount);
    }
}