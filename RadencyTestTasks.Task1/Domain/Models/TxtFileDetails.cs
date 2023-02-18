using System.Text;
using RadencyTestTasks.Task1.Global;
using RadencyTestTasks.Task1.Helpers;
using RadencyTestTasks.Task1.Requests;
using RadencyTestTasks.Task1.Responses;

namespace RadencyTestTasks.Task1.Domain.Models;

public class TxtFileDetails : FileDetails
{
    public override async Task<FileContentResponse> ReadAsync()
    {
        if (!File.Exists(FullFilePath)) throw new FileNotFoundException();

        var data = await File.ReadAllLinesAsync(FullFilePath);
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
                GlobalVariables.FoundErrors++;
            }

            GlobalVariables.ParsedLines++;
        }
        GlobalVariables.ParsedFiles++;
        
        return new FileContentResponse(validTransactions, invalidLinesCount);
    }

    public TxtFileDetails(string fileName, string fullFilePath) 
        : base(fileName, fullFilePath)
    {
    }
}