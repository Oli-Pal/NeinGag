using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [AllowAnonymous]
    public class PhotoController : BaseApiController
    {
        private readonly IPhotoService _photoService;
        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;

        }

        [AllowAnonymous]
        [HttpGet("photos")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPhotos([FromQuery] UserParams userParams)
        {
            try
            {
                var photos = await _photoService.GetPhotosAsync(userParams);

                Response.AddPaginationHeader(photos.CurrentPage, photos.PageSize, photos.TotalCount, photos.TotalCount);

                return Ok(photos);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [AllowAnonymous]
        [HttpGet("popular-photos")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPopularPhotos([FromQuery] UserParams userParams)
        {

            try
            {
                var photos = await _photoService.GetPopularPhotosAsync(userParams);

                Response.AddPaginationHeader(photos.CurrentPage, photos.PageSize, photos.TotalCount, photos.TotalCount);

                return Ok(photos);
            }
            catch (Exception)
            {

                throw;
            }

        }
        //get do loadingu w detail meme

        [HttpGet("get-photo/{id}")]
        public async Task<ActionResult<PhotoDto>> GetPhotoByIdAsync(int id)
        {
            var photo = await _photoService.GetPhotoByIdAsync(id);
            return Ok(photo);
        }

        [Authorize]
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto([FromForm] PhotoUpdateDto photoUpdateDto)
        {
            try
            {
                var addPhoto = await _photoService.AddPhotoService(photoUpdateDto, User.GetUsername());


                return Ok();
            }
            catch (Exception)
            {

                throw;
            }

        }

        [Authorize]
        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            try
            {
                var deletePhoto = await _photoService.DeletePhotoService(photoId, User.GetUsername());


                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}