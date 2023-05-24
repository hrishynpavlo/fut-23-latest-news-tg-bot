using FUT_23_LATEST_NEWS.FeedFetcher;
using FUT_23_LATEST_NEWS.FeedFetcher.Fetchers;
using FUT_23_LATEST_NEWS.FeedFetcher.Parsers;
using Serilog;
using Serilog.Events;

var logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IHtmlFeedFetcher, PuppeteerFetcher>();
builder.Services.AddSingleton<IHtmlParser, FutwizParser>();
builder.Services.AddSingleton<IFeedProvider, FutwizFeedProvider>();
builder.Host.UseSerilog(logger);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
