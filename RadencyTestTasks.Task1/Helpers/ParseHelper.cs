using System.Globalization;
using System.Text.RegularExpressions;
using RadencyTestTasks.Task1.Constraints;
using RadencyTestTasks.Task1.Domain.Models;
using RadencyTestTasks.Task1.Requests;
using RadencyTestTasks.Task1.Responses;

namespace RadencyTestTasks.Task1.Helpers;

public static class ParseHelper
{
    public static PaymentTransactionRequest ParsePaymentTransaction(string data)
    {
        data = data.Replace(" ", string.Empty)
                   .Replace("“", string.Empty);
        
        var words = data.Split(',');

        var transaction = new PaymentTransactionRequest(
            words[0],
            words[1],
            words[2] + ", " + words[3] + ", " + words[4], 
            decimal.Parse(words[5], CultureInfo.InvariantCulture),
            DateOnly.ParseExact(words[6], "yyyy-dd-MM"), 
            long.Parse(words[7], CultureInfo.InvariantCulture),
            words[8]
        );
        
        return transaction;
    }

    public static List<PaymentTransactionResponse> MapToPaymentTransactionResponse(this FileContentResponse source)
    {
        return source.ValidTransactions
            .GroupBy(t => string.Concat(t.Address.TakeWhile(x => x != ',')))
            .Select(t => new PaymentTransactionResponse
            {
                City = t.Key,
                Services = t.DistinctBy(t => t.Service).Select(x => new Service
                {
                    Name = x.Service,
                    Payers = t.Where(y => y.Service == x.Service).Select(z => new Payer
                    {
                        Name = $"{z.FirstName} {z.LastName}",
                        Payment = z.Payment,
                        Date = z.Date,
                        AccountNumber = z.AccountNumber
                    }).ToList(),
                    Total = t.Where(t => x.Service == t.Service).Sum(t => x.Payment)
                }).ToList(),
                Total = t.Sum(t => t.Payment)
            }).ToList();
    }
    public static bool ValidateRow(string row) => GlobalConstraints.RowValidationRegex.IsMatch(row);
}