namespace API.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int CommenterId { get; set; }
        public int CommentedPhotoId { get; set; }
        public string ContentOf { get; set; }
    }
}