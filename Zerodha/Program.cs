using Zerodha;
using Zerodha.Models;
using Zerodha.ServiceContracts;
using Zerodha.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFinhubService, FinhubService>();
 var result = builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("StockOptionName"));
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
//app.MapGet("/", () => "Hello World!");/
app.MapControllers();

app.Run();
