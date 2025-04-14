namespace Zafaty.Server.Dtos
{
    namespace Zafaty.Server.DTOs
    {
        public class BudgetDto
        {
            public int Id { get; set; }
            public int UserId { get; set; } // ✅ إصلاح المشكلة بإضافة معرف المستخدم
            public string BudgetName { get; set; } = string.Empty;
            public decimal TotalBudget { get; set; }
        }
    }

}
