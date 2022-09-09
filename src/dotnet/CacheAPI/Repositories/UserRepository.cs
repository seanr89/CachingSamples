
using Microsoft.Extensions.Caching.Memory;

namespace CacheAPI.Repositories;

public class UserRepository
{
    private readonly IMemoryCache _memoryCache;

    public UserRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

}