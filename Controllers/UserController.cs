using MealPlanBackend.Models;
using MealPlanBackend.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("")]
    public async Task<ActionResult<List<User>>> Get()
    {
        return await _userService.GetUsersAsync();
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<User>> Get(string userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"User with Id = {userId} not found");
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post([FromBody] User user)
    {
        var createdUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(Get), new { userId = createdUser.UserId }, createdUser);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> Put(string userId, [FromBody] User user)
    {
        var updated = await _userService.UpdateUserAsync(userId, user);
        if (!updated)
        {
            return NotFound($"User with Id = {userId} not found");
        }
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> Delete(string userId)
    {
        var deleted = await _userService.RemoveUserAsync(userId);
        if (!deleted)
        {
            return NotFound($"User with Id = {userId} not deleted");
        }
        return Ok();
    }

    
}
