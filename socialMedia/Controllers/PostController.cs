using Microsoft.AspNetCore.Mvc;
using socialMedia.Data;
using socialMedia.Models;
using socialMedia.Service;

namespace socialMedia.Controllers;
[ApiController]
[Route("api/posts")]
public class PostController : Controller {
    
    private readonly PostService _postService;
    private readonly TokenService _tokenService;

    public PostController(ApplicationDb context,IConfiguration configuration)
    {
        _postService = new PostService(context);
        var jwtSettings = configuration.GetSection("JwtSettings");
        _tokenService = new TokenService(
            jwtSettings["SecretKey"],
            jwtSettings["Issuer"],
            jwtSettings["Audience"],
            Convert.ToDouble(jwtSettings["ExpirationMinutes"])
        );
    }

    [HttpPost("create-post")]
    public async Task<IActionResult> CreatePost([FromHeader] string token,[FromBody] Content content)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        var post =  await _postService.CreatePost(content.ContentRequest, username);
        if (post is null)
            return StatusCode(500,"Internal Server Error");
        return Ok(post);
    }

    [HttpPut("edit-post/{id}")]
    public async Task<IActionResult> EditPost([FromHeader] string token,[FromRoute] string id ,[FromBody] Content content)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        var post = await _postService.EditPost(username, int.Parse(id), content.ContentRequest);
        if (post == null)
            return BadRequest("Post not found");
        return Ok(post);
    }

    [HttpDelete("delete-post/{id}")]
    public async Task<IActionResult> DeletePost([FromHeader] string token, [FromRoute] string id) {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        Response response = await _postService.DeletePost(username, int.Parse(id));
        if (response == Models.Response.Success) 
            return Ok();
        if (response == Models.Response.NotFound)
            return BadRequest("post not found");
        return StatusCode(500,"Internal Server Error");
    }

    [HttpGet("my-posts")]
    public async Task<IActionResult> MyPosts([FromHeader] string token)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        var posts = await _postService.GetPosts(username);
        if (posts == null)
            return StatusCode(500,"Internal Server Error");
        return Ok(posts);
    }

    [HttpGet("my-feed")]
    public async Task<IActionResult> MyFeed([FromHeader] string token)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        var response = await _postService.GetFeedPosts(username);
        if(response==null)
            return StatusCode(500,"Internal Server Error");
        return Ok(response);
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetPostsByUsername([FromHeader] string token, [FromRoute] string username) 
    {
        var myUsername = _tokenService.ExtractUsernameFromToken(token);
        if (myUsername == null)
            return Unauthorized();
        var posts = await _postService.GetPosts(username);
        if (posts == null)
            return BadRequest("Username not found");
        return Ok(posts);
    }
        
    
}