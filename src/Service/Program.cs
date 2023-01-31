using Microsoft.Extensions.Logging.Console;
using Npgsql;
using Service.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<NpgsqlConnection>(_ => new NpgsqlConnection(builder.Configuration.GetDbConnectionString()));
builder.Services.AddLogging(c =>
{
    c.AddSimpleConsole(d =>
    {
        d.ColorBehavior = LoggerColorBehavior.Disabled;
        d.SingleLine = true;
    });
});
var app = builder.Build();

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