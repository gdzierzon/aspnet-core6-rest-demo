using Expense.API.Data.EF;
using Expense.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Expense.API.Services;

public class ExpenseService: IExpenseService
{
    private ExpenseContext db;

    public ExpenseService(ExpenseContext db)
    {
        this.db = db;
    }

    public async Task<List<Data.EF.Expense>> GetAll()
    {
        return await db.Expenses.ToListAsync();
    }

    public async Task<List<Data.EF.Expense>> GetListByCategory(string subCategory)
    {
        return await db.Expenses
                       .Where(e => e.SubCategory.Title == subCategory)
                       .ToListAsync();
    }

    public async Task<ExpenseListResponse> GetFilteredList(ExpenseFilter filter)
    {
        var requestedPage = filter.Page.Value;
        var currentPage = requestedPage - 1;

        var pageSize = filter.PageSize.Value;

        var toSkip = currentPage * pageSize;

        try
        {
            var expenseCount = db.Expenses
                                 .Count(e => (!filter.StartDate.HasValue || e.ExpenseDate > filter.StartDate) &&
                                             (!filter.EndDate.HasValue || e.ExpenseDate < filter.EndDate) &&
                                             (!filter.MemberId.HasValue || e.MemberId == filter.MemberId.Value) &&
                                             (filter.Title == null || e.Title.Contains(filter.Title)) &&
                                             (!filter.SubCategoryId.HasValue || e.SubCategoryId == filter.SubCategoryId.Value));
            
            var expenses = await db.Expenses
                                   .Where(e => (!filter.StartDate.HasValue || e.ExpenseDate > filter.StartDate) &&
                                               (!filter.EndDate.HasValue || e.ExpenseDate < filter.EndDate) &&
                                               (!filter.MemberId.HasValue || e.MemberId == filter.MemberId.Value) &&
                                               (filter.Title == null || e.Title.Contains(filter.Title)) &&
                                               (!filter.SubCategoryId.HasValue || e.SubCategoryId == filter.SubCategoryId.Value))
                                   .OrderBy(e => e.SubCategory.Category.Title)
                                   .ThenBy(e => e.ExpenseDate)
                                   .Skip(toSkip)
                                   .Take(pageSize)
                                   .ToListAsync();

            //create next and previous links
            var totalPages = expenseCount / pageSize;
            var remainder = expenseCount % pageSize;
            if (remainder > 0) totalPages++;

            string nextPage = null;
            if (requestedPage < totalPages)
            {
                nextPage = filter.GetPageLink(requestedPage + 1);
            }

            string previousPage = null;
            if (requestedPage > 1)
            {
                previousPage = filter.GetPageLink(requestedPage - 1);
            }


            var response = new ExpenseListResponse()
                           {
                               TotalCount = expenseCount,
                               TotalPages = totalPages,
                               CurrentPage = requestedPage,
                               Data = expenses,
                               Next = nextPage,
                               Previous = previousPage
                           };

            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Data.EF.Expense?> GetById(int id)
    {
        return await db.Expenses.FirstOrDefaultAsync(e => e.ExpenseId == id);
    }

    public async Task<Data.EF.Expense> Add(Data.EF.Expense expense)
    {
        try
        {
            await db.Expenses.AddAsync(expense);
            await db.SaveChangesAsync();

            return expense;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Data.EF.Expense> Update(int id, Data.EF.Expense expense)
    {
        db.Expenses.Update(expense);
        await db.SaveChangesAsync();

        return expense;
    }

    public async Task Delete(int id)
    {
        var expense = await db.Expenses.FirstOrDefaultAsync(e => e.ExpenseId == id);
        if (expense == null)
        {
            throw new ArgumentException("Expense not found.");
        }

        db.Expenses.Remove(expense);
        await db.SaveChangesAsync();
    }
}