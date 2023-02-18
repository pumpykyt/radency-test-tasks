using RadencyTestTasks.Task1.Global;
using RadencyTestTasks.Task1.Helpers;
using RadencyTestTasks.Task1.Requests;
using RadencyTestTasks.Task1.Responses;

namespace RadencyTestTasks.Task1.Domain.Models;

public class CsvFileDetails : FileDetails
{
    public override async Task<FileContentResponse> ReadAsync()
    {
        if (!File.Exists(FullFilePath)) throw new FileNotFoundException();

        var data = await File.ReadAllLinesAsync(FullFilePath);
        var validTransactions = new List<PaymentTransactionRequest>();
        int invalidLinesCount = 0;
        
        data = data.Skip(1)
            .ToArray();

        foreach (var row in data)
        {
            if (ParseHelper.ValidateRow(row))
            {
                var transaction = ParseHelper.ParsePaymentTransaction(row);
                validTransactions.Add(transaction);
            }
            else
            {
                invalidLinesCount++;
                GlobalVariables.FoundErrors++;
                GlobalVariables.InvalidFileNames.Add(FileName);
            }
            GlobalVariables.ParsedLines++;
        }
        GlobalVariables.ParsedFiles++;
        
        return new FileContentResponse(validTransactions, invalidLinesCount);
    }

    public CsvFileDetails(string fileName, string fullFilePath) 
        : base(fileName, fullFilePath)
    {
    }
}