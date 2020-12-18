using API.Entities;

namespace API.DTOs
{
    public class LikeDto
    {
         public int LikerId { get; set; }
        public int LikedId { get; set; }
        public AppUser Liker { get; set; }
        public Photo Liked { get; set; }
    }
}