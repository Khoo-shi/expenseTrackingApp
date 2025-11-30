public class Expense
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }

    // FK Relationship
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
