using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Expense.API.Data.EF;

[Table("SubCategory")]
public class SubCategory
{
    [Key]
    public int SubCategoryId { get; set; }
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }
    [JsonIgnore]
    public List<Expense> Expenses { get; set; }

    public SubCategory()
    {
        Expenses = new List<Expense>();
    }
}