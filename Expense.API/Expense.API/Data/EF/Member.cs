using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Expense.API.Data.EF;

[Table("Member")]
public class Member
{
    [Key]
    public int MemberId { get; set; }
    [Column("MemberName")]
    public string Name { get; set; }

    [JsonIgnore]
    public List<Expense> Expenses { get; set; }

    public Member()
    {
        Expenses = new List<Expense>();
    }
}