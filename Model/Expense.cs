namespace Zafaty.Server.Model
{
    public class Expense
    {
        public int Id { get; set; } // معرف النفقات
        public int BudgetId { get; set; } // المفتاح الأجنبي
        public string Category { get; set; }
        public decimal Amount { get; set; } // المبلغ
        public string? Note { get; set; } // ملاحظة اختيارية
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // العلاقة مع الميزانية
        public Budget Budget { get; set; }
    }

}
