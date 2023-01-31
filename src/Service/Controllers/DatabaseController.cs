using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Service.Controllers;

[ApiController]
[Route("[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly NpgsqlConnection _connection;
    
    public DatabaseController(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    [HttpGet]
    public async Task<List<string>> Get()
    {
        await _connection.OpenAsync();
        
        var command = new NpgsqlCommand("select datname from pg_database", _connection);
        var reader = await command.ExecuteReaderAsync();
        var result = new List<string>();
        while (await reader.ReadAsync())
        {
            result.Add(reader.GetString(0));
        }

        await _connection.CloseAsync();
        return result;
    }
}