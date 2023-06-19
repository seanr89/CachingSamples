using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace RedisAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IMemoryCache memoryCache;
    //private readonly ApplicationDbContext context;
    private readonly IDistributedCache distributedCache;

    //private readonly ILogger<CustomerController> _logger;
    private Customer[] _testCustomers = {
        new Customer(1, "Sean", "Rafferty"),
        new Customer(2, "Ark", "Knight")
    };

    public CustomerController(IMemoryCache memoryCache, IDistributedCache distributedCache)
    {
        this.memoryCache = memoryCache;
        this.distributedCache = distributedCache;
    }

    [HttpGet("redis")]
    public async Task<IActionResult> GetAllCustomersUsingRedisCache()
    {
        var cacheKey = "customerList";
        string serializedCustomerList;
        var customerList = new List<Customer>();
        var redisCustomerList = await distributedCache.GetAsync(cacheKey);
        if (redisCustomerList != null)
        {
            serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
            customerList = JsonConvert.DeserializeObject<List<Customer>>(serializedCustomerList);
        }
        else
        {
            return BadRequest();
            // customerList = await context.Customers.ToListAsync();
            // serializedCustomerList = JsonConvert.SerializeObject(customerList);
            // redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
            // var options = new DistributedCacheEntryOptions()
            //     .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
            //     .SetSlidingExpiration(TimeSpan.FromMinutes(2));
            // await distributedCache.SetAsync(cacheKey, redisCustomerList, options);
        }
        return Ok(customerList);
    }

    [HttpPost("addcustomer")]
    public async Task<IActionResult> Post()
    {
        var cacheKey = "customerList";
        var serializedCustomerList = JsonConvert.SerializeObject(_testCustomers);
        var redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
        var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));
        await distributedCache.SetAsync(cacheKey, redisCustomerList, options);

        return Ok();
    }
}
