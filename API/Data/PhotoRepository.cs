using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PhotoRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

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


        
        public async Task<PhotoDto> GetPhotoByIdAsync(int id)
        {

            return await _context.Photos
            .Where(x => x.Id == id)
            .ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }
    }
}