using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using API.Data;
using System;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;
        private readonly IUserRepository _userRepository;
        private readonly IPhotoRepository _photoRepository;
        public PhotoService(IOptions<CloudinarySettings> config, DataContext context, IMapper mapper, IUserRepository userRepository, IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;

            _userRepository = userRepository;
            _mapper = mapper;
            _context = context;

            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream)
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }


        public async Task<PhotoDto> AddPhotoService([FromForm] PhotoUpdateDto photoUpdateDto, string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            var result = await AddPhotoAsync(photoUpdateDto.File);

            if (result.Error != null) throw new Exception(result.Error.Message);

            var photo = new Photo
            {

                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                Description = photoUpdateDto.Description
            };

            user.Photos.Add(photo);

            if (await _userRepository.SaveAllAsync())
            {

                return _mapper.Map<PhotoDto>(photo);


            }


            throw new Exception("Problem addding photo");
        }
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }

        public async Task<IAsyncResult> DeletePhotoService(int photoId, string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) throw new Exception("Photo doesn't exist");

            if (photo.PublicId != null)
            {
                var result = await DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) throw new Exception(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if (await _userRepository.SaveAllAsync()) return Task.CompletedTask;

            throw new Exception("Failed to delete the photo");


        }



        public async Task<PagedList<PhotoDto>> GetPhotosAsyncService(UserParams userParams)
        {
            var q = await _photoRepository.GetPhotosAsync(userParams);

           return q;
        }

        public async Task<PagedList<PhotoDto>> GetPopularPhotosAsyncService(UserParams userParams)
        {
            var popular = await _photoRepository.GetPopularPhotosAsync(userParams);

            return popular;
        }

        public async Task<PhotoDto> GetPhotoByIdAsyncService(int id)
        {

            var photo = await _photoRepository.GetPhotoByIdAsync(id);
            return photo;
        }
    }
}