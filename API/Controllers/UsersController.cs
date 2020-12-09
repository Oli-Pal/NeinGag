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
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public UsersController(IUserRepository userRepository, IMapper mapper, 
            IPhotoService photoService)
        {
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            
            return Ok(users);
        }

        [HttpGet("photos")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPhotos([FromQuery]UserParams userParams)
        {
            var photos = await _userRepository.GetPhotosAsync(userParams);

            Response.AddPaginationHeader(photos.CurrentPage, photos.PageSize, photos.TotalCount, photos.TotalCount);

            return Ok(photos);
        }
        
        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }

        // [HttpPut]
        // public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        // {

        //     var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        //     _mapper.Map(memberUpdateDto, user);

        //     _userRepository.Update(user);

        //     if (await _userRepository.SaveAllAsync()) return NoContent();

        //     return BadRequest("Failed to update user");
        // }

        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
          
            // var desc = await _userRepository.GetDescriptionOfPhotoAsync();
           
            var result = await _photoService.AddPhotoAsync(file);
            

            if (result.Error != null) return BadRequest(result.Error.Message);

            var photo = new Photo
            {
                
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId,
                // Description = desc.Description
                //Likers
            };

            user.Photos.Add(photo);

            if (await _userRepository.SaveAllAsync())
            {
                return CreatedAtRoute("GetUser", new {username = user.UserName} ,_mapper.Map<PhotoDto>(photo));
            }
                

            return BadRequest("Problem addding photo");
        }

        // [HttpPut("set-main-photo/{photoId}")]
        // public async Task<ActionResult> SetMainPhoto(int photoId)
        // {
        //     var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

        //     var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);


        //     if (await _userRepository.SaveAllAsync()) return NoContent();

        //     return BadRequest("Failed to set main photo");
        // }

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

        // /api/users/ ---
        [HttpPost("{id}/like/{photoId}")]
        public async Task<IActionResult> LikeUser(int id, int photoId)
        {
            //List<int> likeList = _userRepository.GetPhotoLikes(photoId);
            var like = await _userRepository.GetLike(id, photoId);
            if(like != null)
                return BadRequest("You already liked that photo");
            if(await _userRepository.GetPhotoByIdAsync(photoId) == null)
                return NotFound();

            like = new Like
            {
                LikerId = id,
                LikeeId = photoId
            };

            _userRepository.Add<Like>(like);
            if (await _userRepository.SaveAllAsync())
                return Ok();
            return BadRequest("Failed to like");
        }

        [HttpPost("{id}/likes")]
        public async Task<IActionResult> GetNumberOfPhotoLikes(int id)
        {
            var x = await _userRepository.GetNumberOfPhotoLikes(id);
            
            return Ok(x);
        }
    }
}