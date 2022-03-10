using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense.API.Models.Security;

[Table("User")]
public class User
{
    [Key]
    [Column("UserId")]
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}