using LogSwitchWebApp;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RouteOptions>(opt=>opt.LowercaseUrls = true);
builder.AddSerilogWithSwitch();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapControllers();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => DateTimeOffset.UtcNow.ToUnixTimeSeconds());

app.Run();

