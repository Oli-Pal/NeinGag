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
        [AllowAnonymous]
        [HttpGet("photos")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPhotos([FromQuery]UserParams userParams)
        {
            var photos = await _userRepository.GetPhotosAsync(userParams);

            Response.AddPaginationHeader(photos.CurrentPage, photos.PageSize, photos.TotalCount, photos.TotalCount);

            return Ok(photos);
        }
         [HttpGet("photos/{username}")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetUserPhotos(string username)
        {
            var photos = await _userRepository.GetUserPhotosAsync(username);
            return Ok(photos);
        }
        
        [HttpGet("{username}", Name = "GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }


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
                return CreatedAtRoute("GetUser", new {username = user.UserName} ,_mapper.Map<PhotoDto>(photo));
            }
                

            return BadRequest("Problem addding photo");
        }


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

        // /api/users/idusera/like/idzdjecia ---
        [HttpPost("{id}/like/{photoId}")]
        public async Task<IActionResult> LikeUser(int id, int photoId)
        {
            //List<int> likeList = _userRepository.GetPhotoLikes(photoId);
            var like = await _userRepository.GetLike(id, photoId);
            var dislike = await _userRepository.GetDisLike(id, photoId);
            if(like != null)
                _userRepository.Delete<Like>(like);

            if(dislike != null)
                _userRepository.Delete<DisLike>(dislike);

            if(await _userRepository.GetPhotoByIdAsync(photoId) == null)
                return NotFound();

            if(like == null){
            like = new Like
            {
                LikerId = id,
                LikedId = photoId
            };

            _userRepository.Add<Like>(like);}
            if (await _userRepository.SaveAllAsync())
                return Ok();
            return BadRequest("Failed to like");
        }

        [HttpGet("{id}/likes")]
        public async Task<IActionResult> GetNumberOfPhotoLikes(int id)
        {
            var x = await _userRepository.GetNumberOfPhotoLikes(id);
            
            return Ok(x);
        }
    

        // ---------- dislikes -----------
        // /api/users/ ---
        [HttpPost("{id}/dislike/{photoId}")]
        public async Task<IActionResult> DisLikeUser(int id, int photoId)
        {
            //List<int> likeList = _userRepository.GetPhotoLikes(photoId);
            var dislike = await _userRepository.GetDisLike(id, photoId);
            var like = await _userRepository.GetLike(id, photoId);
            if(dislike != null)
                _userRepository.Delete<DisLike>(dislike);
            if(like != null)
                _userRepository.Delete<Like>(like);

            if(await _userRepository.GetPhotoByIdAsync(photoId) == null)
                return NotFound();

            if(dislike == null){
                dislike = new DisLike
            {
                DisLikerId = id,
                DisLikedId = photoId
            };

            _userRepository.Add<DisLike>(dislike);}
            if (await _userRepository.SaveAllAsync())
                return Ok();
            return BadRequest("Failed to like");
        }

        [HttpGet("{id}/dislikes")]
        public async Task<IActionResult> GetNumberOfPhotoDisLikes(int id)
        {
            var x = await _userRepository.GetNumberOfPhotoDisLikes(id);
            
            return Ok(x);
        }
    // COMMENTS
        // /api/users/ ---
        [HttpPost("{id}/comment/{photoId}")]
        public async Task<IActionResult> CommentUser(int id, int photoId,[FromForm] CommentDto commentdto)
        {
            //List<int> likeList = _userRepository.GetPhotoLikes(photoId);
            var comment = await _userRepository.GetComment(id, photoId);
            if(comment != null)
                _userRepository.Delete<Comment>(comment);
            if(await _userRepository.GetPhotoByIdAsync(photoId) == null)
                return NotFound();

            if(comment == null){
                comment = new Comment
            {
                CommenterId = id,
                CommentedPhotoId = photoId,
                Content = commentdto.Content
            };

            _userRepository.Add<Comment>(comment);}
            if (await _userRepository.SaveAllAsync())
                return Ok();
            return BadRequest("Failed to like");
        }

        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetNumberOfPhotoComments(int id)
        {
            var x = await _userRepository.GetNumberOfPhotoComments(id);
            
            return Ok(x);
        }

       
    }
}