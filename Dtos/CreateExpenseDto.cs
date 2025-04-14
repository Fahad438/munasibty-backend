namespace Zafaty.Server.Dtos
{
    public class CreateExpenseDto
    {
      
        public string Category { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
    }
}
