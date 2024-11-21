using Microsoft.EntityFrameworkCore;
using socialMedia.Data;
using socialMedia.Models;

namespace socialMedia.Service;

public class PostService
{
    private readonly ApplicationDb _db;

    public PostService(ApplicationDb db)
    {
        _db = db;

    }

    public async Task<Post?> CreatePost(string content, string username) {
        var post = new Post {
            Username = username,
            Content = content,
            Date = DateTime.Now,
            Likes = 0
        };
        try {
            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();
            return post;

        }
        catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }

    }

    public async Task<Post?> EditPost(string username,int id,string content) {
        try {
            var post =  await _db.Posts.SingleOrDefaultAsync(p => p.IdPost == id && p.Username == username);
            if (post == null)
                return null;
            post.Content = content;
            await _db.SaveChangesAsync();
            return post;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

    }

    public async Task<Response> DeletePost(string username,int id)
    {
        try {
            var post =  await _db.Posts.FirstOrDefaultAsync(p => p.IdPost == id && p.Username == username);
            if (post is null)
                return Response.NotFound;
            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
            return Response.Success;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Response.Error;
        }

    }
    public async Task<List<Post>?> GetPosts(string username) {
        try {
            var isExcist = await _db.Users.AnyAsync(u => u.Username == username);
            if (isExcist == false)
                return null;
            var posts = await _db.Posts
                .Where(post => post.Username == username)
                .OrderByDescending(post => post.IdPost) 
                .Take(20) 
                .ToListAsync();
            posts.Sort((post1, post2) => post2.Date.CompareTo(post1.Date));
            return posts;
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
            return null;
        }        
    }

    public async Task<List<Post>?> GetFeedPosts(string username) {
        try {
            var following = await _db.Follows
                .Where(f => f.Username == username)
                .Select(f => f.Following)
                .ToListAsync();
            var posts = await _db.Posts
                .Where(post => following.Contains(post.Username))
                .OrderByDescending(post => post.IdPost) 
                .Take(20) 
                .ToListAsync();
            posts.Sort((post1, post2) => post2.Date.CompareTo(post1.Date));
            return posts;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}