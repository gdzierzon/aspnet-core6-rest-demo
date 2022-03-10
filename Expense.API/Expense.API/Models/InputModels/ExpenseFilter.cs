using Microsoft.AspNetCore.Mvc;

namespace Expense.API.Models;

public class ExpenseFilter
{
    [FromQuery(Name = "page")] 
    public int? Page { get; set; }
    
    [FromQuery(Name="size")]
    public int? PageSize { get; set; }
    
    [FromQuery(Name="title")]
    public string? Title { get; set; }
    
    [FromQuery(Name="cat")]
    public int? CategoryId { get; set; }
    
    [FromQuery(Name="subcat")]
    public int? SubCategoryId { get; set; }
    
    [FromQuery(Name="member")]
    public int? MemberId { get; set; }
    
    [FromQuery(Name="start")]
    public DateTime? StartDate { get; set; }
    
    [FromQuery(Name = "end")]
    public DateTime? EndDate { get; set; }

    public string? BaseUrl { get; set; }

    public ExpenseFilter()
    {
        if (!Page.HasValue) Page = 1;
        if (!PageSize.HasValue) PageSize = 10;
    }

    private Boolean HasQueryString
    {
        get
        {
            return Page.Value > 1
                   || PageSize.Value != 10
                   || Title != null
                   || CategoryId.HasValue
                   || SubCategoryId.HasValue
                   || MemberId.HasValue
                   || StartDate.HasValue
                   || EndDate.HasValue;
        }
    }

    private string QueryString
    {
        get
        {
            var queryString = "";

            if (HasQueryString)
            {
                if (PageSize.Value != 10) queryString += $"size={PageSize}&";
                if (Title != null) queryString += $"title={Title}&";
                if (CategoryId.HasValue) queryString += $"cat={CategoryId}&";
                if (SubCategoryId.HasValue) queryString += $"subcat={SubCategoryId}&";
                if (MemberId.HasValue) queryString += $"member={MemberId}&";
                if (StartDate.HasValue) queryString += $"start={StartDate.Value: yyyy-MM-dd)}&";
                if (EndDate.HasValue) queryString += $"start={EndDate.Value: yyyy-MM-dd)}&";

                if(queryString.Length > 0)
                    queryString = queryString.Substring(0, queryString.Length - 1);
            }

            return queryString;
        }
    }

    public String GetPageLink(int page)
    {
        return $"{BaseUrl}?page={page}&{QueryString}";
    }

}