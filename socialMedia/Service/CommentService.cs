using Microsoft.EntityFrameworkCore;
using socialMedia.Data;
using socialMedia.Models;

namespace socialMedia.Service;

public class CommentService
{
    private readonly ApplicationDb _db;

    public CommentService(ApplicationDb db) {
        _db = db;
    }

    public async Task<List<Comment>?> GetCommentsByPostId(int postId)
    {
        try {
            var comments = await _db.Comments
                .Where(c => c.IdPost == postId)
                .OrderByDescending(c => c.Id)
                .Take(20)
                .ToListAsync();
            comments.Sort((c1,c2) => c2.Date.CompareTo(c1.Date));
             return comments;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }

    }

    public async Task<Comment?> AddComment(string username,int idPost, string commentContent) {
        var isExcist = await _db.Posts.SingleOrDefaultAsync(p => p.IdPost == idPost);
        if (isExcist == null)
            return null;
        var comment = new Comment {
            IdPost = idPost,
            Username = username,
            Content = commentContent,
            Date = DateTime.Now,
            Likes = 0,
        };
        try {
            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();
            return comment;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Comment?> EditComment(string username, int idComment, string commentContent) {
        try {
            var comment = await _db.Comments.SingleOrDefaultAsync(c => c.Id == idComment &&c.Username == username);
            if (comment == null)
                return null;
            comment.Content = commentContent;
            await _db.SaveChangesAsync();
            return comment;

        }
        catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }

    }

    public async Task<Response> DeleteComment( string username ,int idComment) {
        try {
            var comment = await _db.Comments.SingleOrDefaultAsync(c => c.Id == idComment && c.Username == username);
            if(comment == null)
                return Response.NotFound;
            _db.Comments.Remove(comment);
            await _db.SaveChangesAsync();
            return Response.Success;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Response.Error;
        }

    }
}