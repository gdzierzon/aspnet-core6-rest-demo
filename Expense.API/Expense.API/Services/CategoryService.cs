using Expense.API.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace Expense.API.Services;

public class CategoryService: ICategoryService
{
    private ExpenseContext db;

    public CategoryService(ExpenseContext db)
    {
        this.db = db;
    }

    public async Task<List<Category>> GetAll()
    {
        return await db.Categories.ToListAsync();
    }

    public async Task<Category> GetById(int id)
    {
        return await db.Categories.FirstAsync(c => c.CategoryId == id);
    }

    public async Task<Category> Add(Category category)
    {
        await db.Categories.AddAsync(category);
        await db.SaveChangesAsync();

        return category;
    }

    public async Task<Category> Update(int id, Category category)
    {
        category.CategoryId = id;
        db.Categories.Update(category);
        await db.SaveChangesAsync();

        return category;
    }

    public async Task DeleteById(int id)
    {
        var category = await db.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
        db.Categories.Remove(category);
        await db.SaveChangesAsync();
    }

}