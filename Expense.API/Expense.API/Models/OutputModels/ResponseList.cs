namespace Expense.API.Models;

public class ResponseList<T>
{
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public string Previous { get; set; }
    public string Next { get; set; }
    public List<T> Data { get; set; }   
}