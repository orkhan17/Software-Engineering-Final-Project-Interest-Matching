using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.API.Data.IRepository;
using Project.API.DTOs;
using Project.API.Models;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountForRegisterDto accountForRegisterDto)
        {
            accountForRegisterDto.Username = accountForRegisterDto.Username.ToLower();

            if (await _repo.UserExists(accountForRegisterDto.Username))
                return BadRequest("Username already exists");

            var accountCreate = _mapper.Map<Account>(accountForRegisterDto);

            var createdUser = await _repo.Register(accountCreate, accountForRegisterDto.Password);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AccountForLoginDto accountForLoginDto)
        {
            var accountFromRepo = await _repo.Login(accountForLoginDto.Username.ToLower(), accountForLoginDto.Password);

            if (accountFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, accountFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, accountFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var account = _mapper.Map<Account>(accountFromRepo);
            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                account
            });
        }
    }
}