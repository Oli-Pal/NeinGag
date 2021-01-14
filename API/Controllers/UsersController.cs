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

        private readonly IUsersService _usersService;
        public UsersController(IUserRepository userRepository, IUsersService usersService)
        {
            _usersService = usersService;
            _userRepository = userRepository;
          
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

        [HttpGet("{id}/dislikes")]
        public async Task<IActionResult> GetNumberOfPhotoDisLikes(int id)
        {
              try
            {
                var dislike = await _usersService.GetNumberOfPhotoDisLikesService(id);

                return Ok(dislike);
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
                
                var dislikeIt = await _usersService.DisLikeUserService(id, photoId);

                return Ok();
            }
            catch (Exception)
            {
                
                throw ;
            }
        }

       
        [HttpGet("photos/{username}")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetUserPhotos(string username)
        {
           try
            {
                
                var userPhotos = await _usersService.GetUserPhotosService(username);

                return Ok(userPhotos);
            }
            catch (Exception)
            {
                
                throw ;
            }
        }

        
        //???????
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



       


    }
}

