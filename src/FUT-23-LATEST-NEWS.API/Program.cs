using FUT_23_LATEST_NEWS.API;
using FUT_23_LATEST_NEWS.FeedFetcher;
using FUT_23_LATEST_NEWS.FeedFetcher.Fetchers;
using FUT_23_LATEST_NEWS.FeedFetcher.Parsers;
using FUT_23_LATEST_NEWS.Notifier.Telegram;
using Serilog;
using Serilog.Events;

var logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var appSettings = AppSettings.FromConfig(config);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(appSettings);
builder.Services.AddSingleton<IHtmlFeedFetcher, PuppeteerFetcher>();
builder.Services.AddSingleton<IHtmlParser, FutwizParser>();
builder.Services.AddSingleton<IFeedProvider, FutwizFeedProvider>();
builder.Services.AddSingleton<IFutContentNotifier>(container =>
{
    var appSettings = container.GetService<AppSettings>();
    return new TelegramFutContentNotifier(appSettings.TelegramToken, appSettings.Recipients, container.GetService<ILogger<IFutContentNotifier>>());
}); 
builder.Host.UseSerilog(logger);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
