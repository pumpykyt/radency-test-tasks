using RadencyTestTasks.Task1.Domain.Models;

namespace RadencyTestTasks.Task1.Responses;

public class PaymentTransactionResponse
{
    public string City { get; set; }
    public List<Service> Services { get; set; }
    public decimal Total { get; set; }
}