using Zafaty.Server.Dtos;
using Zafaty.Server.Dtos.Zafaty.Server.DTOs;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _budgetRepository;

        public BudgetService(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public async Task<IEnumerable<BudgetDto>> GetAllBudgetsAsync()
        {
            var budgets = await _budgetRepository.GetAllBudgetsAsync();
            return budgets.Select(b => new BudgetDto
            {
                Id = b.Id,
                UserId=b.UserId,
                BudgetName = b.BudgetName,
                TotalBudget = b.TotalBudget
            });
        }

        public async Task<BudgetDto> GetBudgetByIdAsync(int id)
        {
            var budget = await _budgetRepository.GetBudgetByIdAsync(id);
            if (budget == null) return null;

            return new BudgetDto
            {
                Id = budget.Id,
                UserId = budget.UserId,
                BudgetName = budget.BudgetName,
                TotalBudget = budget.TotalBudget
            };
        }

        public async Task<BudgetDto> CreateBudgetAsync(CreateBudgetDto dto)
        {
            var budget = new Budget
            {
                UserId = dto.UserId,
                BudgetName = dto.BudgetName,
                TotalBudget = dto.TotalBudget
            };

            var newBudget = await _budgetRepository.AddBudgetAsync(budget);
            return new BudgetDto
            {
                Id = newBudget.Id,
                UserId=newBudget.UserId,
                BudgetName = newBudget.BudgetName,
                TotalBudget = newBudget.TotalBudget
            };
        }

        public async Task<BudgetDto> UpdateBudgetAsync(int id, CreateBudgetDto dto)
        {
            var existingBudget = await _budgetRepository.GetBudgetByIdAsync(id);
            if (existingBudget == null) return null;

            existingBudget.BudgetName = dto.BudgetName;
            existingBudget.TotalBudget = dto.TotalBudget;

            var updatedBudget = await _budgetRepository.UpdateBudgetAsync(existingBudget);
            return new BudgetDto
            {
                Id = updatedBudget.Id,
                UserId= updatedBudget.UserId,
                BudgetName = updatedBudget.BudgetName,
                TotalBudget = updatedBudget.TotalBudget
            };
        }

        public async Task<bool> DeleteBudgetAsync(int id)
        {
            return await _budgetRepository.DeleteBudgetAsync(id);
        }
        // عمليات النفقات
        public async Task<ExpenseDto> AddExpenseAsync(int budgetId, CreateExpenseDto expense)
        {
            var newExpense = new Expense
            {
                BudgetId = budgetId,
                Category = expense.Category,
                Amount = expense.Amount,
                Note = expense.Note
            };

            var addedExpense = await _budgetRepository.AddExpenseAsync(budgetId, newExpense);

            return new ExpenseDto
            {
                Id = addedExpense.Id,
                BudgetId = addedExpense.BudgetId,
                Category = addedExpense.Category,
                Amount = addedExpense.Amount,
                Note = addedExpense.Note
            };
        }

        public async Task<bool> RemoveExpenseAsync(int expenseId)
        {
            return await _budgetRepository.RemoveExpenseAsync(expenseId);
        }

        public async Task<ExpenseDto?> UpdateExpenseAsync(int expenseId, CreateExpenseDto expenseDto)
        {
            var existingExpense = await _budgetRepository.GetExpenseByIdAsync(expenseId);
            if (existingExpense == null) return null;

            // تحديث البيانات
            existingExpense.Category = expenseDto.Category;
            existingExpense.Amount = expenseDto.Amount;
            existingExpense.Note = expenseDto.Note;

            var updatedExpense = await _budgetRepository.UpdateExpenseAsync(existingExpense);

            return new ExpenseDto
            {
                Id = updatedExpense.Id,
                BudgetId = updatedExpense.BudgetId,
                Category = updatedExpense.Category,
                Amount = updatedExpense.Amount,
                Note = updatedExpense.Note
            };
        }
        public async Task<IEnumerable<ExpenseDto>> GetExpensesByBudgetIdAsync(int budgetId)
        {
            var expenses = await _budgetRepository.GetExpensesByBudgetIdAsync(budgetId);
            return expenses.Select(e => new ExpenseDto
            {
                Id = e.Id,
                BudgetId = e.BudgetId,
                Category = e.Category,
                Amount = e.Amount,
                Note = e.Note
            }).ToList();
        }

        public async Task<ExpenseDto?> GetExpenseByIdAsync(int expenseId)
        {
            var expense = await _budgetRepository.GetExpenseByIdAsync(expenseId);
            if (expense == null) return null;

            return new ExpenseDto
            {
                Id = expense.Id,
                BudgetId = expense.BudgetId,
                Category = expense.Category,
                Amount = expense.Amount,
                Note = expense.Note
            };
        }


    }
}
