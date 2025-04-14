using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Zafaty.Server.Dtos;
using Zafaty.Server.Dtos.Zafaty.Server.DTOs;
using Zafaty.Server.Interfaces;
using Zafaty.Server.Model;

namespace Zafaty.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [EnableRateLimiting("UserRateLimit")]

    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetDto>>> GetBudgets()
        {
            var budgets = await _budgetService.GetAllBudgetsAsync();
            return Ok(budgets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BudgetDto>> GetBudget(int id)
        {
            var budget = await _budgetService.GetBudgetByIdAsync(id);
            if (budget == null) return NotFound();
            return Ok(budget);
        }

        [HttpPost]
        public async Task<ActionResult<BudgetDto>> CreateBudget(CreateBudgetDto dto)
        {
            var budget = await _budgetService.CreateBudgetAsync(dto);
            return CreatedAtAction(nameof(GetBudget), new { id = budget.Id ,userId= budget.UserId }, budget);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BudgetDto>> UpdateBudget(int id, CreateBudgetDto dto)
        {
            var updatedBudget = await _budgetService.UpdateBudgetAsync(id, dto);
            if (updatedBudget == null) return NotFound();
            return Ok(updatedBudget);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBudget(int id)
        {
            var deleted = await _budgetService.DeleteBudgetAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
        // -------------------- نقاط نهاية النفقات (Expenses) --------------------

        // 🔹 إضافة نفقة إلى ميزانية
        [HttpPost("{budgetId}/expenses")]
        public async Task<IActionResult> AddExpense(int budgetId, [FromBody] CreateExpenseDto expense)
        {
            if (expense == null)
                return BadRequest("Invalid expense data");

            var addedExpense = await _budgetService.AddExpenseAsync(budgetId, expense);
            return CreatedAtAction(nameof(GetBudget), new { id = budgetId }, addedExpense);
        }

        // 🔹 حذف نفقة
        [HttpDelete("expenses/{expenseId}")]
        public async Task<IActionResult> RemoveExpense(int expenseId)
        {
            var result = await _budgetService.RemoveExpenseAsync(expenseId);
            if (!result)
                return NotFound("Expense not found");

            return NoContent();
        }

        // 🔹 تحديث نفقة
        [HttpPut("expenses/{expenseId}")]
        public async Task<IActionResult> UpdateExpense(int expenseId, [FromBody] CreateExpenseDto expenseDto)
        {
            if (expenseDto == null)
                return BadRequest("Invalid expense data");

            var updatedExpense = await _budgetService.UpdateExpenseAsync(expenseId, expenseDto);
            if (updatedExpense == null)
                return NotFound("Expense not found");

            return Ok(updatedExpense);
        }

        // 🔹 سحب جميع النفقات بناءً على BudgetId
        [HttpGet("{budgetId}/expenses")]
        public async Task<IActionResult> GetExpensesByBudgetId(int budgetId)
        {
            var expenses = await _budgetService.GetExpensesByBudgetIdAsync(budgetId);
            return Ok(expenses);
        }

        // 🔹 سحب نفقة واحدة بناءً على ExpenseId
        [HttpGet("expenses/{expenseId}")]
        public async Task<IActionResult> GetExpenseById(int expenseId)
        {
            var expense = await _budgetService.GetExpenseByIdAsync(expenseId);
            if (expense == null)
                return NotFound("Expense not found");

            return Ok(expense);
        }

    }
}