using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace socialMedia.Models;

public class LikeComment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Comment")]
    public int IdComment { get; set; }
    
    public string Username { get; set; }

    [JsonIgnore]
    public virtual Comment Comment { get; set; }
}