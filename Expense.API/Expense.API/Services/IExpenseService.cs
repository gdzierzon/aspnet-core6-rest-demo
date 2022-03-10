using Expense.API.Models;

namespace Expense.API.Services;

public interface IExpenseService
{
    Task<List<Data.EF.Expense>> GetAll();
    Task<List<Data.EF.Expense>> GetListByCategory(string subCategory);
    Task<ExpenseListResponse> GetFilteredList(ExpenseFilter filter);
    Task<Data.EF.Expense?> GetById(int id);
    Task<Data.EF.Expense> Add(Data.EF.Expense expense);
    Task<Data.EF.Expense> Update(int id, Data.EF.Expense expense);
    Task Delete(int id);
}