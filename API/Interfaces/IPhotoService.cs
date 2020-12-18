  
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);

         Task<PagedList<PhotoDto>> GetPhotosAsync(UserParams userParams);
        Task<PagedList<PhotoDto>> GetPopularPhotosAsync(UserParams userParams);
        Task<PhotoDto> GetPhotoByIdAsync(int id);
    }
}