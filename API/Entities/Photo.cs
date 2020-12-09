using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        public string PublicId { get; set; }

        public AppUser AppUser { get; set; }

        public int AppUserId { get; set; }

        public ICollection<Like> Likers { get; set; }
<<<<<<< HEAD
=======

        public int AmountOfLikes{ get; set; } = 0;
<<<<<<< HEAD
>>>>>>> parent of 391f19b... komicik
=======
>>>>>>> parent of 391f19b... komicik
    }
}