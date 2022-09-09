
using CacheAPI.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CacheAPI.Repositories;
public class UserRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(IMemoryCache memoryCache, ILogger<UserRepository> logger)
    {
        _memoryCache = memoryCache;
        _logger = logger;
    }


    public async Task<List<User>> GetUsersCacheAsync()
    {
        var output = _memoryCache.Get<List<User>>("users");

        if (output is not null){
            _logger.LogInformation("GetUsers - Cached");
            return output;
        } 

        _logger.LogInformation("GetUsers - Not Cached");
        output = new()
        {
            new() { FirstName = "Tim", LastName = "Corey" },
            new() { FirstName = "Sue", LastName = "Storm" },
            new() { FirstName = "Jane", LastName = "Jones" }
        };

        await Task.Delay(1000);
        _memoryCache.Set("users", output, TimeSpan.FromMinutes(5));

        return output;
    }
}