using System.Globalization;
using System.Text.RegularExpressions;
using RadencyTestTasks.Task1.Requests;

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

    public static bool ValidateRow(string row, Regex regex) => regex.IsMatch(row);
}