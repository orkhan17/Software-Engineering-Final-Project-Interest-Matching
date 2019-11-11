using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.API.Data;
using Project.API.Data.IRepository;
using Project.API.DTOs;
using Project.API.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Project.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public PostController(IPostRepository repo,IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _repo = repo;
            _context = context;
        }

        [HttpPost("{userid}")]
        public async Task<IActionResult> AddPost(int userid, PostToAddDto postToAddDto)
        {
            if(userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
                
            postToAddDto.AccountId = userid;
            var post = _mapper.Map<Post>(postToAddDto);
            _repo.Add(post);
            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Adding music preference failed on save");
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _repo.GetPosts();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _repo.GetPost(id);
            return Ok(post);
        }


        [HttpGet("visit/{id}")]
        public async Task<IActionResult> GetVisitedProfiles(int id)
        {
            /*if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();*/
            var visited_profiles = await _repo.GetVisitedProfiles(id);
            
            var ans = preference(id);

            IEnumerable<Id_PercentageDto> firstThreeItems ;
            firstThreeItems = ans.Take(3);
            firstThreeItems = firstThreeItems.OrderBy(i => i.Id);
            var sumOfPercentage = firstThreeItems.Sum(i => i.Percentage);

            if(sumOfPercentage == 0.0)
            {
                var pre = await _repo.GetAccountsPreference(id);
                List<Id_PercentageDto> parts = new List<Id_PercentageDto>();
            
                foreach(var p in pre)
                {
                    parts.Add(new Id_PercentageDto { Id = p.Music_type_id, Percentage = 33});
                }

                firstThreeItems = parts;
            }
            


            var accounts = await _repo.GetAccounts();

            accounts = accounts.Where(i => i.Id != id);

            var accountToReturn = _mapper.Map<IEnumerable<AccountToReturnDto>>(accounts);

            List<Id_PercentageDto> account_percentage = new List<Id_PercentageDto>();
            
            foreach (var account in accounts)
            {
                var music_type = preference(account.Id);
                var percent=0.0;
                foreach(var element in firstThreeItems)
                {
                    foreach(var type in music_type)
                    {
                        if(element.Id == type.Id)
                        {
                            percent+=type.Percentage;
                        }
                    }
                }
                account_percentage.Add(new Id_PercentageDto { Id = account.Id, Percentage = percent});
            }

            account_percentage = account_percentage.OrderByDescending(i => i.Percentage).ToList();

            var dateCriteria = DateTime.Now.Date.AddDays(-7);

            var result = from ap in account_percentage
                    join post in _context.Posts on ap.Id equals post.AccountId
                    where post.Created_date > dateCriteria
                    select new
                    {
                        AccountId = post.AccountId,
                        Text = post.Text,
                        Link = post.Video_link,
                        Date = post.Created_date
                    };
            return Ok(result);


 
        }

        public IEnumerable<Id_PercentageDto> preference(int id)
        {
             var result = from visit in _context.Visited_profiles
                    join music in _context.Music_type_accounts on visit.Following_AccountId equals music.Account_Id
                    join music_name in _context.Music_types on music.Music_type_id equals music_name.Id
                    select new
                    {
                        AccountId = visit.AccountId,
                        Following_AccountId = visit.Following_AccountId,
                        Music_type_id = music.Music_type_id,
                        Music = music_name.Type
                    };
            result = result.Where(i => i.AccountId == id).OrderBy(i => i.Music_type_id);
            
            var result1 = from a in result 
                     group a by a.Music_type_id into g
                     select new
                     {
                         Name = g.Key,
                         Sum = g.Count()
                     };

            result1 = result1.OrderByDescending(i => i.Sum);
            var n = result1.Sum(i => i.Sum);

            List<Id_PercentageDto> parts = new List<Id_PercentageDto>();
            
            foreach (var item in result1)
            {
                parts.Add(new Id_PercentageDto { Id = item.Name, Percentage = item.Sum*100/n});
            }
            return parts; 
        }
    }
}