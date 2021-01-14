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
    [Authorize]
    public class CommentController : BaseApiController
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly IPhotoService _photoService;
        
        private readonly ICommentService _commentService;
        public CommentController(IUserRepository userRepository, IMapper mapper,
        ICommentRepository commentRepository, IPhotoService photoService, ICommentService commentService)
        {
            _commentService = commentService;
            _mapper = mapper;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _photoService = photoService;
        }

    // COMMENTS
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments()
    {
        try
        { 
            var users = await _commentService.GetAllCommentsAsyncService();

            return Ok(users);
            
        }
        catch (Exception)
        {
            
            return BadRequest();
        }
       
    }
    //zwraca komenty po id usera
    [HttpGet("byUser/{id}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetUserComments(int id)
    {
        try
        {
            var users = await _commentService.GetUserCommentsAsyncService(id);

            return Ok(users);
        }
        catch (Exception)
        {
            
            return BadRequest();
        }
      
    }





    [AllowAnonymous]
    [HttpGet("{id}/number")]
    public async Task<IActionResult> GetNumberOfPhotoComments(int id)
    {
        try
        {
            var x = await _commentService.GetNumberOfPhotoCommentsService(id);

            return Ok(x);
        }
        catch (Exception)
        {
            
            return BadRequest();
        }
        
    }

    //zwraca komentarze po id zdjecia
    [HttpGet("{id}/ById")]
    public async Task<IActionResult> GetCommentById(int id)
    {
        try
        {
            var x = await _commentService.GetCommentByIdService(id);

            return Ok(x);
        }
        catch (Exception)
        {
            
            return BadRequest();
        }
        
    }
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsAsync(int id)
    {
        try
        {
            var x = await _commentService.GetCommentsAsyncService(id);

            return Ok(x);
        }
        catch (Exception)
        {
            
            return BadRequest();
        }
        
    }


    // /api/comment/ ---
    [HttpPost("{id}/{photoId}")]
    public async Task<ActionResult<IEnumerable<CommentDto>>> CommentUser(int id, int photoId, [FromForm] CommentDto commentDto)
    {
        try
        {
            var addingComment = await _commentService.CommentUserService(id, photoId, commentDto);
            return Ok();
        }
        catch (Exception)
        {
            
            return BadRequest();
        }
    }

    [HttpDelete("delete/{id}/{commenterId}")]
    public async Task<ActionResult<Commentt>> DeleteComment(int id, int commenterId)
    {
        try
        {
            var delcoms = await _commentService.DeleteCommentService(id, commenterId);
            return Ok();
        }
        catch (Exception)
        {
            
            return BadRequest();
        }
    }
}
}