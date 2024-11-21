using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;

namespace socialMedia.Models;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdPost { get; set; } 
    [ForeignKey("User")]
    public string Username { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public int Likes { get; set; }
    [JsonIgnore]
    public virtual ICollection<LikePost> LikeList { get; set; }
    [JsonIgnore]
    public virtual ICollection<Comment> Comments { get; set; }
}