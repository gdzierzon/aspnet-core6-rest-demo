using Expense.API.Data.EF;

namespace Expense.API.Services;

public interface ISubCategoryService
{
    Task<List<SubCategory>> GetAll();
    Task<List<SubCategory>> GetListByCategoryId(int categoryId);
    Task<SubCategory> GetById(int id);
    Task<SubCategory> Add(SubCategory subCategory);
    Task<SubCategory> Update(int id, SubCategory subCategory);
    Task Delete(int id);
}