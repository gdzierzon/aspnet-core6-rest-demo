using Expense.API.Data.EF;
using Expense.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense.API.Controllers;

[ApiController]
[Route("[controller]")]
public class SubCategoriesController: ControllerBase
{
    private ISubCategoryService service;

    public SubCategoriesController(ISubCategoryService service)
    {
        this.service = service;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var subCategories = await service.GetAll();

        return Ok(subCategories);
    }

    [HttpGet("/categories/{categoryId}/subcategories")]
    public async Task<IActionResult> GetByCategoryId([FromRoute]int categoryId)
    {
        var subCategories = await service.GetListByCategoryId(categoryId);

        return Ok(subCategories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var subCategory = await service.GetById(id);

        return Ok(subCategory);
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] SubCategory subCategory)
    {
        var newCategory = await service.Add(subCategory);

        return Ok(newCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] SubCategory subCategory)
    {
        var updatedCategory = await service.Update(id, subCategory);

        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        await service.Delete(id);

        return NoContent();
    }
}