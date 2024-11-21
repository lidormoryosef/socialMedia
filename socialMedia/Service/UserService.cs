using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using socialMedia.Data;
using socialMedia.Models;

namespace socialMedia.Service;

public class UserService
{
    private readonly ApplicationDb _db;
    private readonly TokenService _tokenService;

    public UserService(ApplicationDb db, IConfiguration configuration) {
        _db = db;
        var jwtSettings = configuration.GetSection("JwtSettings");
        _tokenService = new TokenService(
            jwtSettings["SecretKey"],
            jwtSettings["Issuer"],
            jwtSettings["Audience"],
            Convert.ToDouble(jwtSettings["ExpirationMinutes"])
        );
    }
    public async Task<string?> Login(LoginInfo info) {
        var hash = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(info.Password)));
        try {
            if (await _db.Users.AnyAsync(user => user.Username == info.Username && user.Password == hash))
                return _tokenService.GenerateToken(info.Username);
            return string.Empty;
        }catch (Exception e) {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Response> CreateUser(User user) {
        try {
            if (await _db.Users.AnyAsync(userDb => userDb.Username == user.Username)) 
                return Response.Excist;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Response.Error;
        }
        var hashedPassword = Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(user.Password)));
        var userDb = new User {
            Username = user.Username,
            Password = hashedPassword,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            ImageUrl = user.ImageUrl,
            Age = user.Age,
        };
        try {
            await _db.Users.AddAsync(userDb);
            await _db.SaveChangesAsync();
            return Response.Success;
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return Response.Error;
        }
    }
    
}