using Microsoft.AspNetCore.Mvc;
using socialMedia.Data;
using socialMedia.Models;
using socialMedia.Service;
namespace socialMedia.Controllers;
[ApiController]
[Route("api/user")]
public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(ApplicationDb context,IConfiguration configuration) {
        _userService = new UserService(context, configuration);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginInfo info) {
        if (string.IsNullOrEmpty(info.Username) || string.IsNullOrEmpty(info.Password)) {
            return BadRequest("Username or password are required");
        }
        var token = await _userService.Login(info);
        if (token is null) {
            return BadRequest("Invalid username or password");
        }
        return Ok(token);
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user) {
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password)) {
            return BadRequest("Username or password are required");
        }
        Response response = await _userService.CreateUser(user);
        if (response == Models.Response.Success) 
            return Ok(user);
        if(response == Models.Response.Excist)
            return BadRequest("username already exists, please try again");
        return StatusCode(500,"Internal Server Error");
    }
}