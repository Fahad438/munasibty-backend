namespace Zafaty.Server.Dtos
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public int BudgetId { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
    }
}
