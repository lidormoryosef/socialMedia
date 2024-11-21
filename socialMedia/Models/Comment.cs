using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;

namespace socialMedia.Models;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey("Post")]
    public int IdPost { get; set; }
    public string Username { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public int Likes { get; set; }
    [JsonIgnore]
    public virtual Post Post { get; set; }
    [JsonIgnore]
    public virtual ICollection<LikeComment> LikeList { get; set; }
}