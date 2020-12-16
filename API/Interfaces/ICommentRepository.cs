using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface ICommentRepository
    {
                //comment
        Task<IEnumerable<int>> GetUserComments(int id);
        Task<IEnumerable<int>> GetPhotoComments(int id);
        Task<int> GetNumberOfPhotoComments(int id);
        Task<Commentt> GetComment(int userId, int photoId);
        Task<Commentt> GetCommentById(int id);

        Task<IEnumerable<CommentDto>> GetCommentsAsync(int id);

    }
}