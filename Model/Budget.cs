namespace Zafaty.Server.Model
{
    public class Budget
    {
        public int Id { get; set; } // معرف الميزانية
        public int UserId { get; set; } // المفتاح الأجنبي
        public string BudgetName { get; set; }
        public decimal TotalBudget { get; set; } // إجمالي الميزانية
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // علاقة One-to-Many مع النفقات
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

        // العلاقة مع المستخدم
        public User User { get; set; }
    }
}
