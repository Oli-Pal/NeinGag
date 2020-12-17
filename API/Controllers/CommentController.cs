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
    public class CommentController : BaseApiController
    {
        
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly IPhotoService _photoService;
        public CommentController(IUserRepository userRepository, IMapper mapper, 
             ICommentRepository commentRepository, IPhotoService photoService)
        {
            
            _mapper = mapper;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _photoService = photoService;
        }

        // COMMENTS
        // /api/comment/ ---
        [HttpPost("{id}/{photoId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> CommentUser(int id, int photoId, [FromForm] CommentDto commentDto)
        {
            //List<int> likeList = _userRepository.GetPhotoLikes(photoId);
            var comment = await _commentRepository.GetComment(id, photoId);
            // if(comment != null)
            //     _userRepository.Delete<Comment>(comment);
            if(await _photoService.GetPhotoByIdAsync(photoId) == null)
                return NotFound();

            
                comment = new Commentt
            {
                Id = commentDto.Id,
                CommenterId = id,
                CommentedPhotoId = photoId,
                ContentOf = commentDto.ContentOf
            };

            _userRepository.Add<Commentt>(comment);
            if (await _userRepository.SaveAllAsync())
                return Ok();
            
            return BadRequest("Failed to add comment");
        }

        [HttpGet("{id}/number")]
        public async Task<IActionResult> GetNumberOfPhotoComments(int id)
        {
            var x = await _commentRepository.GetNumberOfPhotoComments(id);
            return Ok(x);
        }

        [HttpGet("{id}/ById")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var x = await _commentRepository.GetCommentById(id);
            
            return Ok(x);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsAsync(int id)
        {
            var x = await _commentRepository.GetCommentsAsync(id);
            
            return Ok(x);
        }


        [HttpDelete("delete/{id}/{commenterId}")]
        public async Task<ActionResult<Commentt>> DeleteComment(int id, int commenterId)
        {
           var user = await _userRepository.GetUserByIdAsync(commenterId);

            //var comment = user.Comments.FirstOrDefault(x => x.Id == id);

            var comment = await _commentRepository.GetCommentById(id);
            if (comment == null) return NotFound();

            //user.Comments.Remove(comment);
            _userRepository.Delete<Commentt>(comment);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete comment");
        }
    }
}