using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace socialMedia.Models;

public class Follow
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [ForeignKey("User")]
    public string Following { get; set; }
    [ForeignKey("User")]
    public string Username { get; set; }
}