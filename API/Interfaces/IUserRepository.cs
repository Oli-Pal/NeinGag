using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IUserRepository
    {
         void Update(AppUser user);

         Task<bool> SaveAllAsync();

        Task<IEnumerable<AppUser>> GetUsersAsync();

        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);

        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string username);

        Task<PagedList<PhotoDto>> GetPhotosAsync(UserParams userParams);
    
        //likeee
        Task<Like> GetLike(int userId, int photoId);

        Task<Photo> GetPhotoByIdAsync(int id);

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
<<<<<<< HEAD
=======

        Task<IEnumerable<int>> GetPhotoLikes(int id);
<<<<<<< HEAD
>>>>>>> parent of 391f19b... komicik
=======
>>>>>>> parent of 391f19b... komicik
    }
}