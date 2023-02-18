using RadencyTestTasks.Task1.Responses;

namespace RadencyTestTasks.Task1.Domain.Interfaces;

public interface IFileSaveService
{
    Task SaveFileResultAsync(List<PaymentTransactionResponse> data, string directory);
}