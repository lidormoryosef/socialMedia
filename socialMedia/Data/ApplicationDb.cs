using Microsoft.EntityFrameworkCore;
using socialMedia.Models;

namespace socialMedia.Data;

public class ApplicationDb : DbContext
{
    public ApplicationDb(DbContextOptions<ApplicationDb> options): base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<LikePost> LikesToPost { get; set; }
    public DbSet<LikeComment> LikesToComment { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<LikePost>()
            .HasOne(l => l.Post)
            .WithMany(p => p.LikeList) 
            .HasForeignKey(l => l.IdPost)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post) 
            .WithMany(p => p.Comments) 
            .HasForeignKey(c => c.IdPost) 
            .OnDelete(DeleteBehavior.Cascade); 
        modelBuilder.Entity<LikeComment>()
            .HasOne(l => l.Comment)
            .WithMany(c => c.LikeList) 
            .HasForeignKey(l => l.IdComment) 
            .OnDelete(DeleteBehavior.Cascade);  
    }
}