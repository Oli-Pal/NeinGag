using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using System.Collections.Generic;
using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using API.Data;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config,DataContext context, IMapper mapper)
        {
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

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }


        public async Task<PagedList<PhotoDto>> GetPhotosAsync(UserParams userParams)
        {
            var query = _context.Photos
            .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(x => x.Id)
            .AsNoTracking();

            return await PagedList<PhotoDto>
            .CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

         public async Task<PagedList<PhotoDto>> GetPopularPhotosAsync(UserParams userParams)
        {
            var query = _context.Photos
            .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(x => (x.Likers.Count - x.DisLikers.Count));

            return await PagedList<PhotoDto>
            .CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

         public async Task<PhotoDto> GetPhotoByIdAsync(int id)
        {
            
            return await _context.Photos
            .Where(x => x.Id == id)
            .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }
    }
}