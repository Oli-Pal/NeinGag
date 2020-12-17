using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CommentRepository : ICommentRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CommentRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        //COMMENTS ---------------------------------------------
        public async Task<IEnumerable<int>> GetUserComments(int id)
        {
            var user = await _context.Users
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user.Comments.Where(u => u.CommenterId == id).Select(i => i.Id);
        }

            public async Task<IEnumerable<int>> GetPhotoComments(int id)
        {
            var user = await _context.Photos
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user.Comments.Where(u => u.CommentedPhotoId == id).Select(i => i.Id);
        }

             public async Task<int> GetNumberOfPhotoComments(int id)
        {
            var likes = await _context.Comments.Where(x => x.CommentedPhotoId == id).ToListAsync();

            return likes.Count;
        }
  
        public async Task<Commentt> GetComment(int userId, int photoId)
        {
            return await _context.Comments
            .FirstOrDefaultAsync(u => u.CommenterId == userId && u.CommentedPhotoId == photoId);
        }

        public async Task<Commentt> GetCommentById(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(u => u.Id == id);
        }

         public async Task<IEnumerable<CommentDto>> GetCommentsAsync(int id)
        {
            return await _context.Comments
                    .Where(p => p.CommentedPhotoId == id)
            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<IEnumerable<Commentt>> GetAllCommentsAsync()
        {
            return await _context.Comments
            .ToListAsync();
        }
        public async Task<IEnumerable<CommentDto>> GetUserCommentsAsync(int id)
        {
            return await _context.Comments
            .Where(p => p.CommenterId == id)
            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }
    }
}