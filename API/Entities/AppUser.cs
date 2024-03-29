using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }    

        public byte[] PasswordSalt { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string NickName { get; set; }

        public DateTime Created { get; set; }  = DateTime.Now;

        public DateTime LastActive { get; set; }  = DateTime.Now;

        public string Country { get; set; }

        public ICollection<Photo> Photos { get; set; }

       // public ICollection<Like> Likers { get; set; } 
        public ICollection<Like> Likees { get; set; }
        public ICollection<DisLike> DisLikees { get; set; } 

        public ICollection<Commentt> Comments { get; set; } 
        
        public int Amount { get; set; }
        
        
        
        // public int GetAge(){
            
        //     return DateOfBirth.CalculateAge();
        // }
    }
}