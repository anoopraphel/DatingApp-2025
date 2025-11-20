using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class AccountController(AppDbContext context, ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDTOs register)
        {
            if (await EmailExist(register.Email)) return BadRequest("Email taken");
            var hmac = new HMACSHA512();
            var user = new AppUser
            {
                DisplayName = register.DisplayName,
                Email = register.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user.ToDto(tokenService);
        }

        private async Task<bool> EmailExist(string email)
        {
            return context.Users.Any(x => x.Email.ToLower() == email.ToLower());
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDtos loginDtos)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Email.ToLower() == loginDtos.Email.ToLower());

            if (user == null) return Unauthorized("Invalid Email address");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDtos.Password));

            for (var i = 0; i < computeHash.Length; i++)
            {
                if (computeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return user.ToDto(tokenService);
        }
    }
}
