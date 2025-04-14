using Zafaty.Server.Dtos;
using Zafaty.Server.Dtos.Zafaty.Server.DTOs;
using Zafaty.Server.Model;

namespace Zafaty.Server.Interfaces
{
    public interface IBudgetService
    {
        Task<IEnumerable<BudgetDto>> GetAllBudgetsAsync();
        Task<BudgetDto> GetBudgetByIdAsync(int id);
        Task<BudgetDto> CreateBudgetAsync(CreateBudgetDto dto);
        Task<BudgetDto> UpdateBudgetAsync(int id, CreateBudgetDto dto);
        Task<bool> DeleteBudgetAsync(int id);


        // عمليات النفقات
        Task<ExpenseDto> AddExpenseAsync(int budgetId, CreateExpenseDto expense);
        Task<bool> RemoveExpenseAsync(int expenseId);
        Task<ExpenseDto?> UpdateExpenseAsync(int expenseId, CreateExpenseDto expense);
        Task<IEnumerable<ExpenseDto>> GetExpensesByBudgetIdAsync(int budgetId);
        Task<ExpenseDto?> GetExpenseByIdAsync(int expenseId);


    }
}
