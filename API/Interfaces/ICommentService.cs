using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface ICommentService
    {
         Task<int> GetNumberOfPhotoCommentsService(int id);
         Task<Commentt> GetCommentService(int userId, int photoId);
         Task<Commentt> GetCommentByIdService(int id);
         Task<IEnumerable<CommentDto>> GetCommentsAsyncService(int id);
         Task<IEnumerable<Commentt>> GetAllCommentsAsyncService();

         Task<IEnumerable<CommentDto>> GetUserCommentsAsyncService(int id);

         Task<IAsyncResult> CommentUserService(int id, int photoId, [FromForm] CommentDto commentDto);
         Task<IAsyncResult> DeleteCommentService(int id, int commenterId);
    }
}