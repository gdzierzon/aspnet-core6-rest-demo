using Expense.API.Data.EF;

namespace Expense.API.Services;

public interface ICategoryService
{
    Task<List<Category>> GetAll();
    Task<Category> GetById(int id);
    Task<Category> Add(Category category);
    Task<Category> Update(int id, Category category);
    Task DeleteById(int id);
}