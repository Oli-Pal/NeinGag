
using System;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<PhotoDto> AddPhotoService([FromForm] PhotoUpdateDto photoUpdateDto, string username);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<IAsyncResult> DeletePhotoService(int photoId, string username);

         Task<PagedList<PhotoDto>> GetPhotosAsync(UserParams userParams);
        Task<PagedList<PhotoDto>> GetPopularPhotosAsync(UserParams userParams);
        Task<PhotoDto> GetPhotoByIdAsync(int id);
        
    }
}