using RadencyTestTasks.Task1.Requests;

namespace RadencyTestTasks.Task1.Responses;

public class FileContentResponse
{
    public List<PaymentTransactionRequest> ValidTransactions { get; set; }
    public int InvalidLinesCount { get; set; }

    public FileContentResponse(List<PaymentTransactionRequest> validTransactions, int invalidLinesCount)
    {
        ValidTransactions = validTransactions;
        InvalidLinesCount = invalidLinesCount;
    }
}