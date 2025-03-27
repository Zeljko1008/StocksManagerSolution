using ServiceContracts;
using Services;
using StocksAppConfigAssignment;
using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;
using Repositories;
using Serilog;
using Serilog.AspNetCore;
using StocksAppConfigAssignment.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider serviceProvider, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(serviceProvider);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<StockOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddHttpClient<FinnHubGetCompanyProfileService>();
builder.Services.AddScoped<IFinnHubGetStocksService, FinnHubGetStocksService>();
builder.Services.AddScoped<IFinnHubGetCompanyProfileService, FinnHubGetCompanyProfileService>();
builder.Services.AddScoped<IFinnHubSearchStockService, FinnHubSearchStockService>();
builder.Services.AddScoped<IFinnHubStockPriceQuoteService, FinnHubStockPriceQuoteService>();
builder.Services.AddScoped<IStockServiceCreateOrder, StocksServiceCreateOrder>();
builder.Services.AddScoped<IStockServiceGetters, StocksServiceGetters>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IFinnHubRepository, FinnhubRepository>();

builder.Services.AddDbContext<StockMarketDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseSerilogRequestLogging();
if (builder.Environment.IsDevelopment())
{
   // app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/Error");
    app.UseExceptionHandlingMiddleware();
}


Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
