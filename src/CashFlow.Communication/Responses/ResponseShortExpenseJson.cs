namespace CashFlow.Communication.Responses;

public class ResponseShortExpenseJson
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}