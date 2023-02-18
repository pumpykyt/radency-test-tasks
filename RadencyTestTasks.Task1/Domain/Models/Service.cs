namespace RadencyTestTasks.Task1.Domain.Models;

public class Service
{
    public string Name { get; set; }
    public List<Payer> Payers { get; set; }
    public decimal Total { get; set; }
}