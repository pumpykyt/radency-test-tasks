using RadencyTestTasks.Task1.Requests;

namespace RadencyTestTasks.Task1.Interfaces;

public interface IFileContext
{
    Task<List<PaymentTransactionRequest>> ReadTransactionsFromFileAsync(string fileName);
    void Test(string data);
}