using Expense.API.Data.EF;
using Expense.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController: ControllerBase
{
    private ICategoryService service;

    public CategoriesController(ICategoryService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await service.GetAll();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        var category = await service.GetById(id);

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] Category category)
    {
        var newCategory = await service.Add(category);

        return Ok(newCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute]int id, [FromBody] Category category)
    {
        var updatedCategory = await service.Update(id, category);

        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        await service.DeleteById(id);

        return NoContent();
    }
}