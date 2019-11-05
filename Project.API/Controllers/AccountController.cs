using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.API.Data.IRepository;
using Project.API.DTOs;
using Project.API.Models;

namespace Project.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repo;
        private readonly IMapper _mapper;
        public AccountController(IAccountRepository repo,IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _repo.GetAccounts();

            var accountToReturn = _mapper.Map<IEnumerable<AccountToReturnDto>>(accounts);
            return Ok(accountToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _repo.GetAccount(id);

            var accountToReturn = _mapper.Map<AccountToReturnDto>(account);
            return Ok(accountToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, AccountForUpdateDto accountForUpdateDto)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var accountFromRepo = await _repo.GetAccount(id);

            _mapper.Map(accountForUpdateDto, accountFromRepo);

            if(await _repo.SaveAll())
            {
                var accountToReturn = _mapper.Map<AccountToReturnDto>(accountFromRepo);
                return Ok(accountToReturn);
            }
               
            
            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var accountFromRepo = await _repo.GetAccount(id);
            if(accountFromRepo != null)
            {
                 _repo.Delete(accountFromRepo);
            }
            if(await _repo.SaveAll()) {
                return Ok();
            }
            return BadRequest();
             
        }
    }
}