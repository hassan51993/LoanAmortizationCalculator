using LoanAmortizationCalculator.Infrastructure.Data;
using LoanAmortizationCalculator.Domain.Repositories;
using LoanAmortizationCalculator.Infrastructure.Repositories;
using LoanAmortizationCalculator.Application.Services;
using LoanAmortizationCalculator.API.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;
using LoanAmortizationCalculator.Application.Commands.CreateLoan;
using LoanAmortizationCalculator.Application.Queries.GetLoan;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext to use SQL Server
builder.Services.AddDbContext<LoanAmortizationContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LoanAmortizationDatabase")));

// Register repositories
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<LoanService>();

// Add MediatR and register all handlers from the Application assembly
builder.Services.AddMediatR(typeof(CreateLoanCommandHandler).GetTypeInfo().Assembly);
builder.Services.AddMediatR(typeof(GetLoanQuery).GetTypeInfo().Assembly);

var app = builder.Build();

// Use custom exception handling middleware
app.UseExceptionHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
