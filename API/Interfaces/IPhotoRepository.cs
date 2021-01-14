using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;

namespace API.Interfaces
{
    public interface IPhotoRepository
    {
         Task<PagedList<PhotoDto>> GetPhotosAsync(UserParams userParams);
         Task<PagedList<PhotoDto>> GetPopularPhotosAsync(UserParams userParams);
         Task<PhotoDto> GetPhotoByIdAsync(int id);
    }
}