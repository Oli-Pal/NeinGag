using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoService _photoService;
        public UsersService(IUserRepository userRepository, IPhotoService photoService)
        {
            _photoService = photoService;
            _userRepository = userRepository;

        }


        public async Task<IEnumerable<MemberDto>> GetUsersService()
        {
            var users = await _userRepository.GetMembersAsync();
            return users.ToList();
        }

        public async Task<MemberDto> GetUserService(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }


        public async Task<IEnumerable<LikeDto>> GetAllLikesService(int id)
        {
            var likes = await _userRepository.GetUserLikesAsync(id);

            return likes;
        }

        public async Task<int> GetNumberOfPhotoLikesService(int id)
        {
            var like = await _userRepository.GetNumberOfPhotoLikes(id);

            return like;
            
        }

        public async Task<int> GetNumberOfPhotoDisLikesService(int id)
        {
            var dislike = await _userRepository.GetNumberOfPhotoDisLikes(id);

            return dislike;
        }



        public async Task<IAsyncResult> LikeUserService(int id, int photoId)
        {
            var like = await _userRepository.GetLike(id, photoId);
            var dislike = await _userRepository.GetDisLike(id, photoId);

            if (like != null)
                _userRepository.Delete<Like>(like);

            if (dislike != null)
                _userRepository.Delete<DisLike>(dislike);

            if (await _photoService.GetPhotoByIdAsync(photoId) == null)
                throw new Exception ("Photos doesn't exist");

            if (like == null)
            {
                like = new Like
                {
                    LikerId = id,
                    LikedId = photoId
                };

                _userRepository.Add<Like>(like);
            }
            if (await _userRepository.SaveAllAsync())
                return Task.CompletedTask;
            

            throw new Exception ("Failed to like");
        }

         public async Task<IAsyncResult> DisLikeUserService(int id, int photoId)
        {
            var dislike = await _userRepository.GetDisLike(id, photoId);
            var like = await _userRepository.GetLike(id, photoId);
            if (dislike != null)
                _userRepository.Delete<DisLike>(dislike);
            if (like != null)
                _userRepository.Delete<Like>(like);

            if (await _photoService.GetPhotoByIdAsync(photoId) == null)
                 throw new Exception ("Photos doesn't exist");

            if (dislike == null)
            {
                dislike = new DisLike
                {
                    DisLikerId = id,
                    DisLikedId = photoId

                };

                _userRepository.Add<DisLike>(dislike);
            }
            if (await _userRepository.SaveAllAsync())
                return Task.CompletedTask;
            

            throw new Exception ("Failed to like");
        }

    }
}