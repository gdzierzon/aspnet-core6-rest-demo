using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Expense.API.Data.EF;

[Table("Category")]
public class Category
{
    [Key]
    [Column("CategoryId")]
    public int CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public List<SubCategory> SubCategories { get; set; }

    public Category()
    {
        SubCategories = new List<SubCategory>();
    }
}