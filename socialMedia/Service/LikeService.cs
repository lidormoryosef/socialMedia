using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using socialMedia.Data;
using socialMedia.Models;
using socialMedia.Utils;

namespace socialMedia.Service;

public class LikeService
{
    private ApplicationDb _db;
    public LikeService(ApplicationDb db)
    {
        _db = db;
    }

    public async Task<Response> LikePost(string username, int postId)
    {
        try
        {
            var post = await _db.Posts.SingleOrDefaultAsync(p => p.IdPost == postId);
            if (post == null)
                return Response.NotFound;
            var isExcist = await _db.LikesToPost.FirstOrDefaultAsync(l => l.Username == username && l.IdPost == postId);
            if (isExcist != null)
                return Response.Excist;
            post.Likes++;
            await _db.LikesToPost.AddAsync(new LikePost{IdPost = postId, Username = username});
            await _db.SaveChangesAsync();
            return Response.Success;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Response.Error;
        }
    }

    public async Task<Response> UnLikePost(string username, int postId)
    {
        try {
            var post = await _db.Posts
                .SingleOrDefaultAsync(p => p.IdPost == postId);
            if (post == null)
                return Response.NotFound;
            var like = await _db.LikesToPost
                .SingleOrDefaultAsync(l => l.Username == username && l.IdPost == postId);
            if (like == null)
                return Response.Excist;
            post.Likes--;
            _db.LikesToPost.Remove(like);
            await _db.SaveChangesAsync();
            return Response.Success;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Response.Error;
        }
    }

    public async Task<List<UserInfo>?> GetLikesForPost(int postId)
    {
        try {
            var usernames = await _db.LikesToPost
                .Where( l =>l.IdPost == postId)
                .Select( l => l.Username)
                .ToListAsync();
            return await Utils.Utils.GetUsersInfo(usernames,_db);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }
    }
    public async Task<Response> LikeComment(string username, int commentId)
    {
        try {
            var comment = await _db.Comments.SingleOrDefaultAsync(c => c.Id == commentId);
            if (comment == null)
                return Response.NotFound;
            var isExcist = await _db.LikesToComment
                .SingleOrDefaultAsync(l => l.Username == username && l.IdComment == commentId);
            if (isExcist != null)
                return Response.Excist;
            comment.Likes++;
            await _db.LikesToComment.AddAsync(new LikeComment{IdComment = commentId, Username = username});
            await _db.SaveChangesAsync();
            return Response.Success;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Response.Error;
        }
    }

    public async Task<Response> UnLikeComment(string username, int commentId)
    {
        try {
            var comment = await _db.Comments.SingleOrDefaultAsync(c => c.Id == commentId);
            if (comment == null)
                return Response.NotFound;
            var like = await _db.LikesToComment
                .SingleOrDefaultAsync(l => l.Username == username && l.Id == commentId);
            if (like == null)
                return Response.Excist;
            comment.Likes--;
            _db.LikesToComment.Remove(like);
            await _db.SaveChangesAsync();
            return Response.Success;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Response.Error;
        }
    }

    public async Task<List<UserInfo>?> GetLikesForComment(int commentId)
    {
        try {
            var usernames = await _db.LikesToComment
                .Where( l =>l.IdComment == commentId)
                .Select( l => l.Username)
                .ToListAsync();
            return await Utils.Utils.GetUsersInfo(usernames,_db);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }
    }
}