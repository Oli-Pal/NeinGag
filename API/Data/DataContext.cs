using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions options) : base(options)
        {

        }

        public DbSet<AppUser> Users { get; set; }  //<> class that we want to create 
        public DbSet<Photo> Photos { get; set; }

        public DbSet<UserLike> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLike>()
            .HasKey(k => new {k.SourceUserId, k.LikedPhotoId});

            builder.Entity<UserLike>()
            .HasOne(s => s.SourceUser)
            .WithMany(l => l.LikedPhotos)
            .HasForeignKey(s => s.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserLike>()
            .HasOne(s => s.LikedPhoto)
            .WithMany(l => l.LikedByUsers)
            .HasForeignKey(s => s.LikedPhotoId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}