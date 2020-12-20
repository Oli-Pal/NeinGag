using System;
using System.Collections.Generic;
using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
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
        
        public ICollection<AppUserRole> UserRoles { get; set; }
        
        // public int GetAge(){
            
        //     return DateOfBirth.CalculateAge();
        // }
    }
}