using System.ComponentModel.DataAnnotations;

namespace socialMedia.Models;

public class User
{
    [Key]
    public string Username { get; set; }
    public string Password { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string ImageUrl { get; set; }
    public int Age { get; set; }
}