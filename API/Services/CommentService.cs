using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using System.Linq;
using System.Collections.Generic;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPhotoService _photoService;
        private readonly IUserRepository _userRepository;
        public CommentService(ICommentRepository commentRepository, IPhotoService photoService, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _photoService = photoService;
            _commentRepository = commentRepository;

        }

        public async Task<int> GetNumberOfPhotoCommentsService(int id)
        {
            var nb = await _commentRepository.GetNumberOfPhotoComments(id);

            return nb;
        }


        public async Task<Commentt> GetCommentService(int userId, int photoId)
        {
            var gcomment = await _commentRepository.GetComment(userId, photoId);
            return gcomment;
        }


        public async Task<Commentt> GetCommentByIdService(int id)
        {
            var comId = await _commentRepository.GetCommentById(id);
            return comId;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsAsyncService(int id)
        {
            var comments = await _commentRepository.GetCommentsAsync(id);
            return comments;
        }


        public async Task<IEnumerable<Commentt>> GetAllCommentsAsyncService()
        {
            var allcoms = await _commentRepository.GetAllCommentsAsync();
            return allcoms;
        }

        public async Task<IEnumerable<CommentDto>> GetUserCommentsAsyncService(int id)
        {
            var usercoms = await _commentRepository.GetUserCommentsAsync(id);
            return usercoms;
        }


        public async Task<IAsyncResult> CommentUserService(int id, int photoId, [FromForm] CommentDto commentDto)
        {
            
            var comment = await GetCommentService(id, photoId);

            if (await _photoService.GetPhotoByIdAsyncService(photoId) == null)
                throw new Exception("Photo doesn't exist");


            comment = new Commentt
            {
                Id = commentDto.Id,
                CommenterId = id,
                CommentedPhotoId = photoId,
                ContentOf = commentDto.ContentOf
            };

            _userRepository.Add<Commentt>(comment);
           if (await _userRepository.SaveAllAsync()) return Task.CompletedTask;

            throw new Exception ("Failed to add comment");

        }


        public async Task<IAsyncResult> DeleteCommentService(int id, int commenterId)
    {
        var user = await _userRepository.GetUserByIdAsync(commenterId);

        //var comment = user.Comments.FirstOrDefault(x => x.Id == id);

        var comment = await _commentRepository.GetCommentById(id);
        if (comment == null) throw new Exception ("Comment doesn't exist");

        //user.Comments.Remove(comment);
        _userRepository.Delete<Commentt>(comment);

        if (await _userRepository.SaveAllAsync()) return Task.CompletedTask;

        throw new Exception ("Failed to delete comment");
    }
    }
}