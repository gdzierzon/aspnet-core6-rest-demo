using Expense.API.Data.EF;
using Expense.API.Services;
using Microsoft.EntityFrameworkCore;

const string CORS_POLICY = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ExpenseContext>(options => options.UseSqlServer(connection));

builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();


builder.Services.AddCors(options =>
                 {
                     options.AddPolicy(CORS_POLICY,
                                       builder =>
                                       {
                                           builder.AllowAnyOrigin()
                                                  .AllowAnyMethod()
                                                  .AllowAnyHeader();
                                       });
                 });


var app = builder.Build();

app.UseCors(CORS_POLICY);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
