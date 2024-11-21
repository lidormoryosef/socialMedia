using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace socialMedia.Models;

public class LikePost
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey("Post")]
    public int IdPost { get; set; }
    public string Username { get; set; }
    [JsonIgnore]
    public virtual Post Post { get; set; }

}