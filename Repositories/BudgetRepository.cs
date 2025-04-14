using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly AppDbContext _context;

        public BudgetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Budget>> GetAllBudgetsAsync()
        {
            return await _context.Budget.Include(b => b.Expenses).ToListAsync();
        }

        public async Task<Budget> GetBudgetByIdAsync(int id)
        {
            return await _context.Budget.Include(b => b.Expenses)
                                         .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Budget> AddBudgetAsync(Budget budget)
        {
            _context.Budget.Add(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<Budget> UpdateBudgetAsync(Budget budget)
        {
            _context.Budget.Update(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<bool> DeleteBudgetAsync(int id)
        {
            var budget = await _context.Budget.FindAsync(id);
            if (budget == null) return false;
            _context.Budget.Remove(budget);
            await _context.SaveChangesAsync();
            return true;
        }
        // -------------------- عمليات النفقات داخل الميزانية --------------------

        public async Task<Expense> AddExpenseAsync(int budgetId, Expense expense)
        {
            expense.BudgetId = budgetId;
            _context.Expense.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task<bool> RemoveExpenseAsync(int expenseId)
        {
            var expense = await _context.Expense.FindAsync(expenseId);
            if (expense == null)
                return false;

            _context.Expense.Remove(expense);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Expense?> UpdateExpenseAsync(Expense expense)
        {
            var existingExpense = await _context.Expense.FindAsync(expense.Id);
            if (existingExpense == null)
                return null;

            existingExpense.Category = expense.Category;
            existingExpense.Amount = expense.Amount;
            existingExpense.Note = expense.Note;
            existingExpense.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingExpense;
        }
        public async Task<IEnumerable<Expense>> GetExpensesByBudgetIdAsync(int budgetId)
        {
            return await _context.Expense
                .Where(e => e.BudgetId == budgetId)
                .ToListAsync();
        }

        public async Task<Expense?> GetExpenseByIdAsync(int expenseId)
        {
            return await _context.Expense.FindAsync(expenseId);
        }

    }
}