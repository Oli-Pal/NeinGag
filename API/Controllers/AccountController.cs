using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly IAccountService _accountService;
        public AccountController(DataContext context, IAccountService accountService)
        {
            _accountService = accountService;
        
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            try
            {
                return Ok(await _accountService.RegisterUser(registerDto));
            }
            catch (Exception)
            {
                throw ;
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try
            {
                return Ok(await _accountService.LoginUser(loginDto));
            }
            catch (Exception)
            {
                
                throw ;
            }
        }





    }
}