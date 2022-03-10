using Expense.API.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace Expense.API.Services;

public class SubCategoryService: ISubCategoryService
{
    private ExpenseContext db;

    public SubCategoryService(ExpenseContext db)
    {
        this.db = db;
    }

    public async Task<List<SubCategory>> GetAll()
    {
        return await db.SubCategories
                       .ToListAsync();
    }

    public async Task<List<SubCategory>> GetListByCategoryId(int categoryId)
    {
        return await db.SubCategories
                       .Where(s => s.CategoryId == categoryId)
                       .ToListAsync();
    }

    public async Task<SubCategory> GetById(int id)
    {
        return await db.SubCategories
                       .FirstAsync(s => s.SubCategoryId == id);
    }

    public async Task<SubCategory> Add(SubCategory subCategory)
    {
        await db.SubCategories.AddAsync(subCategory);
        await db.SaveChangesAsync();

        return subCategory;
    }

    public async Task<SubCategory> Update(int id, SubCategory subCategory)
    {
        subCategory.SubCategoryId = id;
        db.SubCategories.Update(subCategory);
        await db.SaveChangesAsync();

        return subCategory;
    }

    public async Task Delete(int id)
    {
        var subCategory = await db.SubCategories
                                  .FirstOrDefaultAsync(s => s.SubCategoryId == id);

        db.SubCategories.Remove(subCategory);
        await db.SaveChangesAsync();
    }
}