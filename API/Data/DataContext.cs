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
        public DbSet<Like> Likes { get; set; }
        public DbSet<DisLike> DisLikes { get; set; }

        public DbSet<Commentt> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>()
                .HasKey(k => new {k.LikerId, k.LikedId});

            builder.Entity<Like>()
                .HasOne(u => u.Liked)
                .WithMany(u => u.Likers)
                .HasForeignKey(u => u.LikedId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(u => u.Likees)
                .HasForeignKey(u => u.LikerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<DisLike>()
                .HasKey(k => new {k.DisLikerId, k.DisLikedId});

            builder.Entity<DisLike>()
                .HasOne(u => u.DisLiked)
                .WithMany(u => u.DisLikers)
                .HasForeignKey(u => u.DisLikedId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DisLike>()
                .HasOne(u => u.DisLiker)
                .WithMany(u => u.DisLikees)
                .HasForeignKey(u => u.DisLikerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Commentt>()
                .HasKey(x => new {x.Id});

            builder.Entity<Commentt>()
                .HasOne(u => u.CommentedPhoto)
                .WithMany(u => u.Comments)
                .HasForeignKey(u => u.CommentedPhotoId);


            builder.Entity<Commentt>()
                .HasOne(u => u.Commenter)
                .WithMany(u => u.Comments)
                .HasForeignKey(u => u.CommenterId);
        }
        
    }
}