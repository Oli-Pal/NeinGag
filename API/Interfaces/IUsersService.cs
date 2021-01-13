using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IUsersService
    {
         Task<IEnumerable<MemberDto>> GetUsersService();
         Task<MemberDto> GetUserService(string username);
         Task<IEnumerable<LikeDto>> GetAllLikesService(int id);
         
          Task<int> GetNumberOfPhotoLikesService(int id);
          Task<IAsyncResult> LikeUserService(int id, int photoId);
          Task<IAsyncResult> DisLikeUserService(int id, int photoId);
    }
}