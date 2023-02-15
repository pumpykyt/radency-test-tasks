namespace RadencyTestTasks.Task1.Requests;

public class PaymentTransactionRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Address { get; init; }
    public decimal Payment { get; init; }
    public DateOnly Date { get; init; }
    public long AccountNumber { get; init; }
    public string Service { get; init; }

    public PaymentTransactionRequest(string firstName, string lastName, string address, decimal payment, DateOnly date, long accountNumber, string service)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Payment = payment;
        Date = date;
        AccountNumber = accountNumber;
        Service = service;
    }
}