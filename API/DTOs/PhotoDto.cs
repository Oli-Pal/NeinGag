using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class PhotoDto
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

         public string Nickname{ get; set; }
        
        public ICollection<Like> Likers { get; set; }

        public int AmountOfLikes{ get; set; }
    }
}