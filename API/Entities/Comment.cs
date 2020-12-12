namespace API.Entities
{
    public class Comment
    {
        public int CommenterId { get; set; }
        public int CommentedPhotoId { get; set; }
        public AppUser Commenter { get; set; }
        public Photo CommentedPhoto { get; set; }
        
        public string ContentOf{ get; set; }
    }
}