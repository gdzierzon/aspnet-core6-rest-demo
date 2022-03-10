using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Expense.API.Data.EF;

[Table("Expense")]
public class Expense
{
    public int ExpenseId { get; set; }
    public int MemberId { get; set; }
    public int SubCategoryId { get; set; }
    public string Title { get; set; }
    public Decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
    public string Description { get; set; }
    public string Vendor { get; set; }
    public string PaymentType { get; set; }
    public bool IsBusinessExpense { get; set; }

    [JsonIgnore]
    public Member? Member { get; set; }
    [JsonIgnore]
    public SubCategory? SubCategory { get; set; }
    
}