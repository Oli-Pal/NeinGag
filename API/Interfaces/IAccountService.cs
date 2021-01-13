using System.Threading.Tasks;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IAccountService
    {
         Task<UserDto> RegisterUser(RegisterDto registerDto);
         Task<UserDto> LoginUser(LoginDto loginDto);
    }
}