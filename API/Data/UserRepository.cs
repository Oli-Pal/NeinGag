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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await _context.Users
                    .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<PagedList<PhotoDto>> GetPhotosAsync(UserParams userParams)
        {
            var query = _context.Photos
            .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(x => x.Id)
            .AsNoTracking();

            return await PagedList<PhotoDto>
            .CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

         public async Task<PagedList<PhotoDto>> GetPopularPhotosAsync(UserParams userParams)
        {
            var query = _context.Photos
            .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
            .OrderByDescending(x => (x.Likers.Count - x.DisLikers.Count));

            return await PagedList<PhotoDto>
            .CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }


//Likeeee ---------------------------------------------
        public async Task<IEnumerable<int>> GetUserLikes(int id)
        {
            var user = await _context.Users
                .Include(x => x.Likees)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user.Likees.Where(u => u.LikerId == id).Select(i => i.LikedId);
        }

    //photolikes - zwracanie listy uzytkownikow ktorzy lubia zdjecie
            public async Task<IEnumerable<int>> GetPhotoLikes(int id)
        {
            var user = await _context.Photos
                .Include(x => x.Likers)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user.Likers.Where(u => u.LikedId == id).Select(i => i.LikerId);
        }

             public async Task<int> GetNumberOfPhotoLikes(int id)
        {
            var likes = await _context.Likes.Where(x => x.LikedId == id).ToListAsync();

            return likes.Count;
        }
  
        public async Task<Like> GetLike(int userId, int photoId)
        {
            return await _context.Likes.FirstOrDefaultAsync(u => u.LikerId == userId && u.LikedId == photoId);
        }
//--------------------------------------
//DISLikeeee ---------------------------------------------
        public async Task<IEnumerable<int>> GetUserDisLikes(int id)
        {
            var user = await _context.Users
                .Include(x => x.DisLikees)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user.DisLikees.Where(u => u.DisLikerId == id).Select(i => i.DisLikedId);
        }

    //photolikes - zwracanie listy uzytkownikow ktorzy lubia zdjecie
            public async Task<IEnumerable<int>> GetPhotoDisLikes(int id)
        {
            var user = await _context.Photos
                .Include(x => x.DisLikers)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user.DisLikers.Where(u => u.DisLikedId == id).Select(i => i.DisLikerId);
        }

             public async Task<int> GetNumberOfPhotoDisLikes(int id)
        {
            var dislikes = await _context.DisLikes.Where(x => x.DisLikedId == id).ToListAsync();

            return dislikes.Count;
        }
  
        public async Task<DisLike> GetDisLike(int userId, int photoId)
        {
            return await _context.DisLikes.FirstOrDefaultAsync(u => u.DisLikerId == userId && u.DisLikedId == photoId);
        }
//--------------------------------------
            public async Task<IEnumerable<PhotoDto>> GetUserPhotosAsync(string username)
        {
            return await _context.Photos
            .Where(p => p.AppUser.UserName == username)
            .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users
            .FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

    

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
            .Include(p => p.Photos)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified; //adds flag to entity that it has been modified
        }

      
        public async Task<PhotoDto> GetPhotoByIdAsync(int id)
        {
            
            return await _context.Photos
            .Where(x => x.Id == id)
            .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
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
       
    }
}