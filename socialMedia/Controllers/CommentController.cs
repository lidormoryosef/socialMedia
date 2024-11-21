using Microsoft.AspNetCore.Mvc;
using socialMedia.Data;
using socialMedia.Models;
using socialMedia.Service;

namespace socialMedia.Controllers;
[ApiController]
[Route("api/comments")]
public class CommentController : Controller {
    private readonly CommentService _commentService;
    private readonly TokenService _tokenService;

    public CommentController(ApplicationDb context,IConfiguration configuration)
    {
        _commentService = new CommentService(context);
        var jwtSettings = configuration.GetSection("JwtSettings");
        _tokenService = new TokenService(
            jwtSettings["SecretKey"],
            jwtSettings["Issuer"],
            jwtSettings["Audience"],
            Convert.ToDouble(jwtSettings["ExpirationMinutes"])
        );
    }

    [HttpGet("commentsByPostId/{postId}")]
    public async Task<IActionResult> GetCommentsByPostId([FromHeader] string token, [FromRoute] string postId)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        var comments = await _commentService.GetCommentsByPostId(int.Parse(postId));
        if (comments == null)
            return StatusCode(500,"Internal Server Error");
        return Ok(comments);
    }

    [HttpPost("create-comment/{idPost}")]
    public async Task<IActionResult> CreateComment([FromHeader] string token ,[FromRoute] string idPost,[FromBody] Content commentContent)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        var comment = await _commentService.AddComment(username, int.Parse(idPost), commentContent.ContentRequest);
        if (comment is null)
            return StatusCode(500,"Internal Server Error");;
        return Ok(comment);
    }
    [HttpPut("edit-comment/{idComment}")]
    public async Task<IActionResult> EditComment([FromHeader] string token ,[FromRoute] string idComment,[FromBody] Content commentContent)
    {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        var comment = await _commentService.EditComment(username, int.Parse(idComment), commentContent.ContentRequest);
        if (comment == null)
            return NotFound("Comment not found");
        return Ok(comment);
    }

    [HttpDelete("delete-comment/{idComment}")]
    public async Task<IActionResult> DeleteComment([FromHeader] string token, [FromRoute] string idComment) {
        var username = _tokenService.ExtractUsernameFromToken(token);
        if (username == null)
            return Unauthorized();
        Response response = await _commentService.DeleteComment(username, int.Parse(idComment));
        if (response == Models.Response.Success)
            return Ok();
        if (response == Models.Response.NotFound)
            return BadRequest("Comment not found");
        return StatusCode(500,"Internal Server Error");;
    }
}