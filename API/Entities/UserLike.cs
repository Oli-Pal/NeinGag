namespace API.Entities
{
    public class UserLike
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }

        //public AppUser LikedUser { get; set; }
        //public int LikedUserId { get; set;}
        public Photo LikedPhoto { get; set; }

        public int LikedPhotoId { get; set; }

    }
}