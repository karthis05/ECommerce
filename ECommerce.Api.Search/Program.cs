using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;
using Microsoft.Extensions.Http;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISearchService,SearchSerivice>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductsService, ProductsService>();

builder.Services.AddHttpClient("OrdersService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Orders"]);
});
builder.Services.AddHttpClient("ProductsService", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Products"]);
}).AddTransientHttpErrorPolicy(p=>p.WaitAndRetryAsync(5,_ => TimeSpan.FromMilliseconds(500)));


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
