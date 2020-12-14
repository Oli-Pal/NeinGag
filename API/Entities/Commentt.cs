using System.Collections.Generic;

namespace API.Entities
{
    public class Commentt
    {
        public int Id { get; set; }
        public int CommenterId { get; set; }
        public int CommentedPhotoId { get; set; }
        public AppUser Commenter { get; set; }
        public Photo CommentedPhoto { get; set; }
        
        public string ContentOf{ get; set; }
    }
}