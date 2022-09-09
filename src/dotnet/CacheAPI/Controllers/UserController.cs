
using CacheAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CacheAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UsersController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Get all users.
    /// </summary>
    // GET api/users
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var values = await _userRepository.GetUsersCacheAsync();
        return Ok(values);
    }
}