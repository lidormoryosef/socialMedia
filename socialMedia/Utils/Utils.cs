using Microsoft.EntityFrameworkCore;
using socialMedia.Data;
using socialMedia.Models;

namespace socialMedia.Utils;

public class Utils
{
    public static async Task<List<UserInfo>> GetUsersInfo(List<string> usernames,ApplicationDb db)
    {
        if (usernames.Count == 0)
            return [];
        var usersInfo = await db.Users.
            Where(user => usernames.Contains(user.Username))
            .Select(user => new UserInfo()
            {
                Username = user.Username,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                ImageUrl = user.ImageUrl
            }).ToListAsync();
        return usersInfo;
    }
}