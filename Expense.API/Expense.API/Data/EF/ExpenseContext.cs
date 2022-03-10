using Expense.API.Models.Security;
using Microsoft.EntityFrameworkCore;

namespace Expense.API.Data.EF;

public class ExpenseContext: DbContext
{

    public ExpenseContext(DbContextOptions<ExpenseContext> options)
        : base(options)
    {

    }

    public DbSet<Member> Members { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }
}
