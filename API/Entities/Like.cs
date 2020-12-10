namespace API.Entities
{
    public class Like
    {
        public int LikerId { get; set; }
        public int LikedId { get; set; }
        public AppUser Liker { get; set; }
        public Photo Liked { get; set; }
    }
}