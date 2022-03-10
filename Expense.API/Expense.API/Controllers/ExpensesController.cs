using Expense.API.Models;
using Expense.API.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Expense.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpensesController : ControllerBase
{
    private IExpenseService expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        this.expenseService = expenseService;
    }

    private string GetBaseUrl()
    {
        var url = this.Request.GetEncodedUrl();
        var queryString = this.Request.QueryString.Value;
        url = url.Replace(queryString, "");

        return url;
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses()
    {
        var result = await expenseService.GetAll();
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var expense = await expenseService.GetById(id);

        if (expense != null)
        {
            return Ok(expense);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> PostExpense([FromBody] Data.EF.Expense expense)
    {
        var newExpense = await expenseService.Add(expense);

        return Ok(newExpense);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutExpense([FromRoute] int id, [FromBody] Data.EF.Expense expense)
    {
        var updatedExpense = await expenseService.Update(id, expense);

        return Ok(updatedExpense);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await expenseService.Delete(id);
            return NoContent();
        }
        catch (ArgumentException e)
        {
            return BadRequest();
        }
        catch (Exception e)
        {
            throw;
        }

    }
}