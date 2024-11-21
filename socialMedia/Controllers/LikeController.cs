using Microsoft.AspNetCore.Mvc;
using socialMedia.Data;
using socialMedia.Models;
using socialMedia.Service;

namespace socialMedia.Controllers;
[ApiController]
[Route("api/like")]
public class LikeController : Controller
{
    private readonly LikeService _likeService;
    private readonly TokenService _tokenService;

    public LikeController(ApplicationDb context,IConfiguration configuration) {
        _likeService = new LikeService(context);
        var jwtSettings = configuration.GetSection("JwtSettings");
        _tokenService = new TokenService(
            jwtSettings["SecretKey"],
            jwtSettings["Issuer"],
            jwtSettings["Audience"],
            Convert.ToDouble(jwtSettings["ExpirationMinutes"])
        );
    }
    [HttpPost("post/{postId}")]
    public async Task<IActionResult> LikePost([FromHeader] string token, [FromRoute] string postId)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        Response response = await _likeService.LikePost(username, int.Parse(postId));
        if (response == Models.Response.Success)
            return Ok();
        if (response == Models.Response.NotFound)
            return BadRequest("Post not found");
        if(response == Models.Response.Excist)
            return BadRequest("You Already Liked this Post");
        return StatusCode(500,"Internal Server Error");
    }

    [HttpDelete("post/{postId}")]
    public async Task<IActionResult> UnlikePost([FromHeader] string token, [FromRoute] string postId)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        Response response = await _likeService.UnLikePost(username, int.Parse(postId));
        if (response == Models.Response.Success)
            return Ok();
        if (response == Models.Response.NotFound)
            return BadRequest("Post not found");
        if(response == Models.Response.Excist)
            return BadRequest("You Already UnLiked this Post");
        return StatusCode(500,"Internal Server Error");
    }

    [HttpGet("post/{postId}")]
    public async Task<IActionResult> GetLikesForPost([FromHeader] string token, [FromRoute] string postId)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if(username == null)
            return Unauthorized();
        var likes = await _likeService.GetLikesForPost(int.Parse(postId));
        if (likes == null)
            return StatusCode(500,"Internal Server Error");
        return Ok(likes);
    }
    [HttpPost("comment/{commentId}")]
    public async Task<IActionResult> LikeComment([FromHeader] string token, [FromRoute] string commentId)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        Response response = await _likeService.LikeComment(username, int.Parse(commentId));
        if (response == Models.Response.Success)
            return Ok();
        if (response == Models.Response.NotFound)
            return BadRequest("Comment not found");
        if(response == Models.Response.Excist)
            return BadRequest("You Already Liked this Comment");
        return StatusCode(500,"Internal Server Error");
    }

    [HttpDelete("comment/{commentId}")]
    public async Task<IActionResult> UnlikeComment([FromHeader] string token, [FromRoute] string commentId)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        Response response = await _likeService.UnLikeComment(username, int.Parse(commentId));
        if (response == Models.Response.Success)
            return Ok();
        if (response == Models.Response.NotFound)
            return BadRequest("Comment not found");
        if(response == Models.Response.Excist)
            return BadRequest("You Already Liked this Comment");
        return StatusCode(500,"Internal Server Error");
        
    }

    [HttpGet("comment/{commentId}")]
    public async Task<IActionResult> GetLikesForComment([FromHeader] string token, [FromRoute] string commentId)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if(username == null)
            return Unauthorized();
        var likes = await _likeService.GetLikesForComment(int.Parse(commentId));
        if (likes == null)
            return StatusCode(500, "Error while getting likes");
        return Ok(likes);
    }
}