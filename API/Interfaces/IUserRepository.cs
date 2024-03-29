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

       
    
        //likeee
        Task<Like> GetLike(int userId, int photoId);

        

        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<IEnumerable<int>> GetPhotoLikes(int id);
        Task<int> GetNumberOfPhotoLikes(int id);
         Task<DisLike> GetDisLike(int userId, int photoId);
        Task<IEnumerable<int>> GetPhotoDisLikes(int id);
        Task<int> GetNumberOfPhotoDisLikes(int id);
        Task<IEnumerable<PhotoDto>> GetUserPhotosAsync(string username);
        Task<IEnumerable<LikeDto>> GetUserLikesAsync(int id);


        
    }
}