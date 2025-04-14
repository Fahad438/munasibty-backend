using Zafaty.Server.Model;

namespace Zafaty.Server.Interfaces
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> GetAllBudgetsAsync();
        Task<Budget> GetBudgetByIdAsync(int id);
        Task<Budget> AddBudgetAsync(Budget budget);
        Task<Budget> UpdateBudgetAsync(Budget budget);
        Task<bool> DeleteBudgetAsync(int id);

        // عمليات النفقات
        Task<Expense> AddExpenseAsync(int budgetId, Expense expense);
        Task<bool> RemoveExpenseAsync(int expenseId);
        Task<Expense?> UpdateExpenseAsync(Expense expense);
        Task<IEnumerable<Expense>> GetExpensesByBudgetIdAsync(int budgetId);
        Task<Expense?> GetExpenseByIdAsync(int expenseId);


    }
}
