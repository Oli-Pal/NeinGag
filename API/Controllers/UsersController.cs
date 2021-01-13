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
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IUsersService _usersService;
        public UsersController(IUserRepository userRepository, IMapper mapper, IUsersService usersService, IPhotoService photoService)
        {
            _usersService = usersService;
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;
            _photoService = photoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            try
            {
                var users = await _usersService.GetUsersService();

                return Ok(users);
            }
            catch (System.Exception)
            {
                
                throw;
            }
            
        }

        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            try
            {
                var user = await _usersService.GetUserService(username);
                return Ok(user);
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }


        [HttpGet("userlikes/{id}")]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetAllLikes(int id)
        {
            try
            {
                var likes = await _usersService.GetAllLikesService(id);

                return Ok(likes);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [HttpGet("{id}/likes")]
        public async Task<ActionResult> GetNumberOfPhotoLikes(int id)
        {
            try
            {
                var like = await _usersService.GetNumberOfPhotoLikesService(id);

                return Ok(like);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [Authorize]
        [HttpPost("{id}/like/{photoId}")]
        public async Task<ActionResult> LikeUser(int id, int photoId)
        {
            try
            {
                
                var likeIt = await _usersService.LikeUserService(id, photoId);

                return Ok();
            }
            catch (Exception)
            {
                
                throw ;
            }
        }



        [Authorize]
        [HttpPost("{id}/dislike/{photoId}")]
        public async Task<ActionResult> DisLikeUser(int id, int photoId)
        {
             try
            {
                
                var dislikeIt = await _usersService.LikeUserService(id, photoId);

                return Ok();
            }
            catch (Exception)
            {
                
                throw ;
            }
        }

       [HttpGet("{id}/dislikes")]
        public async Task<IActionResult> GetNumberOfPhotoDisLikes(int id)
        {
            var x = await _userRepository.GetNumberOfPhotoDisLikes(id);

            return Ok(x);
        }

        [HttpGet("photos/{username}")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetUserPhotos(string username)
        {
            var photos = await _userRepository.GetUserPhotosAsync(username);
            return Ok(photos);
        }


     


       
 



        [AllowAnonymous]
        [HttpGet("photos")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPhotos([FromQuery] UserParams userParams)
        {
            var photos = await _photoService.GetPhotosAsync(userParams);

            Response.AddPaginationHeader(photos.CurrentPage, photos.PageSize, photos.TotalCount, photos.TotalCount);

            return Ok(photos);
        }

        [AllowAnonymous]
        [HttpGet("popular-photos")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPopularPhotos([FromQuery] UserParams userParams)
        {
            var photos = await _photoService.GetPopularPhotosAsync(userParams);

            Response.AddPaginationHeader(photos.CurrentPage, photos.PageSize, photos.TotalCount, photos.TotalCount);

            return Ok(photos);
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

            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());


            var result = await _photoService.AddPhotoAsync(photoUpdateDto.File);

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {

                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                Description = photoUpdateDto.Description
            };

            user.Photos.Add(photo);

            if (await _userRepository.SaveAllAsync())
            {
                return CreatedAtRoute("GetUser", new { username = user.UserName }, _mapper.Map<PhotoDto>(photo));
            }


            return BadRequest("Problem addding photo");
        }
        [Authorize]
        [HttpPost("add-coins/{amount}/{id}")]
        public async Task<ActionResult<MemberDto>> AddCoins(int amount, int id)
        {

            var user = await _userRepository.GetUserByIdAsync(id);
            user.Amount += amount;
            if (await _userRepository.SaveAllAsync())
                return Ok(user);
            return BadRequest("Problem addding photo");
        }



        [Authorize]
        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if (photo == null) return NotFound();

            if (photo.PublicId != null)
            {
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                if (result.Error != null) return BadRequest(result.Error.Message);
            }

            user.Photos.Remove(photo);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete the photo");
        }


    }
}

