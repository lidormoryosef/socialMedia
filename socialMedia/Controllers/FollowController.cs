using Microsoft.AspNetCore.Mvc;
using socialMedia.Data;
using socialMedia.Models;
using socialMedia.Service;

namespace socialMedia.Controllers;
[ApiController]
[Route("api/follow")]
public class FollowController: Controller {
    private readonly FollowService _followService;
    private readonly TokenService _tokenService;

    public FollowController(ApplicationDb context,IConfiguration configuration) {
        _followService = new FollowService(context);
        var jwtSettings = configuration.GetSection("JwtSettings");
        _tokenService = new TokenService(
            jwtSettings["SecretKey"],
            jwtSettings["Issuer"],
            jwtSettings["Audience"],
            Convert.ToDouble(jwtSettings["ExpirationMinutes"])
        );
    }

    [HttpPost]
    public async Task<IActionResult> Follow([FromHeader] string token, [FromBody] Content follow) {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        Response response = await _followService.Follow(username, follow.ContentRequest);
        if (response == Models.Response.Success)
            return Ok();
        if (response == Models.Response.NotFound)
            return BadRequest("Username not found");
        if(response == Models.Response.Excist)
            return BadRequest("You Already Follow this Username");
        return StatusCode(500,"Internal Server Error");
    }
    [HttpDelete]
    public async Task<IActionResult> Unfollow([FromHeader] string token, [FromBody] Content follow) {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        Response response = await _followService.Unfollow(username, follow.ContentRequest);
        if (response == Models.Response.Success)
            return Ok();
        if (response == Models.Response.NotFound)
            return BadRequest("Username not found");
        if(response == Models.Response.Excist)
            return BadRequest("You Already UnFollow this Username");
        return StatusCode(500,"Internal Server Error");
    }

    [HttpGet("get-following")]
    public async Task<IActionResult> GetFollowing([FromHeader] string token) {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        var response = await _followService.GetFollowing(username);
        if(response == null)
            return StatusCode(500,"Internal Server Error");
        return Ok(response);
    }

    [HttpGet("get-followers")]
    public async Task<IActionResult> GetFollowers([FromHeader] string token) {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        var response = await _followService.GetFollowers(username);
        if(response == null)
            return StatusCode(500,"Internal Server Error");
        return Ok(response);
    }
    [HttpGet("get-following/{username}")]
    public async Task<IActionResult> GetFollowingByUsername([FromHeader] string token,[FromRoute] string username) {
        var myUsername = _tokenService.ExtractUsernameFromToken(token);
        if (myUsername == null)
            return Unauthorized();
        var response = await _followService.GetFollowing(username);
        if(response == null)
            return StatusCode(500,"Internal Server Error");
        return Ok(response);
    }

    [HttpGet("get-followers/{username}")]
    public async Task<IActionResult> GetFollowers([FromHeader] string token,[FromRoute] string username) {
        var myUsername = _tokenService.ExtractUsernameFromToken(token);
        if (myUsername == null)
            return Unauthorized();
        var response = await _followService.GetFollowers(username);
        if(response == null)
            return StatusCode(500,"Internal Server Error");
        return Ok(response);
    }
}