using Microsoft.EntityFrameworkCore;
using socialMedia.Data;
using socialMedia.Models;

namespace socialMedia.Service;

public class FollowService {
    private readonly ApplicationDb _db;

    public FollowService(ApplicationDb db) {
        _db = db;
    }

    public async Task<Response>  Follow(string username, string following) {
        if (username.Equals(following))
            return Response.NotFound;
        try {
            var user = await _db.Users.SingleOrDefaultAsync(user => user.Username == following);
            if (user == null) 
                return Response.NotFound;
            var isExcist = await _db.Follows.FirstOrDefaultAsync(f => f.Username == username && f.Following == following);
            if (isExcist != null)
                return Response.Excist;
            var follow = new Follow {
                Username = username,
                Following = following
            };
            await _db.Follows.AddAsync(follow);
            await _db.SaveChangesAsync();
            return Response.Success;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Response.Error;
        }
    }

    public async Task<Response> Unfollow(string username, string following) {
        try {
            var follow = await _db.Follows.FirstOrDefaultAsync(f => f.Following == following && f.Username == username);
            if (follow == null)
                return Response.Excist;
            _db.Follows.Remove(follow);
            await _db.SaveChangesAsync();
            return Response.Success;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Response.Error;
        }
        
    }

    public async Task<List<UserInfo>?> GetFollowing(string username) {
        try {
            var following = await _db.Follows.
                Where(f => f.Username == username)
                .Select(f => f.Following)
                .Take(50)
                .ToListAsync();
            return await Utils.Utils.GetUsersInfo(following,_db);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }
    }
    public async Task<List<UserInfo>?> GetFollowers(string username) {
        try {
            var followers = await _db.Follows
                .Where(f => f.Following == username)
                .Select(f => f.Username)
                .Take(50)
                .ToListAsync();
            return await Utils.Utils.GetUsersInfo(followers,_db);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }
    }
}