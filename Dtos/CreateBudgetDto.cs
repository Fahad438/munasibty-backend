namespace Zafaty.Server.Dtos
{
    public class CreateBudgetDto
    {
        public int UserId { get; set; } 
        public string BudgetName { get; set; }
        public decimal TotalBudget { get; set; }
    }
}
