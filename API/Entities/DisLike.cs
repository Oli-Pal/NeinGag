namespace API.Entities
{
    public class DisLike
    {
        public int DisLikerId { get; set; }
        public int DisLikedId { get; set; }
        public AppUser DisLiker { get; set; }
        public Photo DisLiked { get; set; }
    }
}