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
using System.Configuration;
using Newtonsoft.Json;
using System.Net.Http;

namespace Project.API.Controllers
{
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
            /*if(userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
                */
            postToAddDto.Video_link = postToAddDto.Video_link.Replace("watch?v=","embed/");
            postToAddDto.AccountId = userid;
            var post = _mapper.Map<Post>(postToAddDto);
            _repo.Add(post);
            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Adding music preference failed on save");
        }

        [HttpPost("{userid}/like/{postid}")]
        public async Task<IActionResult> like(int userid, int postid)
        {
          /*  if(userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            */
            var account = await _repo.GetAccount(userid);

            if(account == null) 
                return BadRequest("There is not a user");

            var post = await _repo.GetPost(postid);

            if(post == null) 
                return BadRequest("There is not a post");
            
            var like = await _repo.GetLike(userid,postid);
            
            if(like != null ) 
                return BadRequest("Already liked");

            Post_Like pt = new Post_Like();
            pt.AccountId = userid;
            pt.Post_id = postid;
            _repo.Add(pt);
            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Adding music preference failed on save");
        }

        [HttpPost("{userid}/follow/{followingid}")]
        public async Task<IActionResult> Follow(int userid, int followingid)
        {
           /* if(userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            */
            var account1 = await _repo.GetAccount(userid);

            if(account1 == null) 
                return BadRequest("There is not a user");

            var account2 = await _repo.GetAccount(followingid);

            if(account2 == null) 
                return BadRequest("There is not a user to follow");
            
            var follow = await _repo.GetFollow(userid,followingid);
            
            if(follow != null ) 
                return BadRequest("Already followed");

            Follower f = new Follower();
            f.AccountId = userid;
            f.Following_AccountId = followingid;
            _repo.Add(f);
            if(await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Adding music preference failed on save");
        }

        [HttpPost("{userid}/visit/{otheraccountid}")]
        public async Task<IActionResult> VisitProfile(int userid, int otheraccountid)
        {
            /*if(userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
                */
            var user1 = await _repo.GetAccount(userid);
            var user2 = await _repo.GetAccount(otheraccountid);
            if(user1 == null || user2 == null)
                return BadRequest("There is not a user");
            
            Visited_profile vp = new Visited_profile();
            vp.AccountId = userid;
            vp.Following_AccountId = otheraccountid;
            _repo.Add(vp);
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

        [HttpGet("5post")]
        public async Task<IActionResult> Get5Posts()
        {
            var posts = await _repo.Get5Posts();
            posts = posts.AsEnumerable().Take(5);
            return Ok(posts);
        }

        [HttpGet("{userid}/following")]
        public async Task<IActionResult> GetFollowing(int userid)
        {
            var followings = await _repo.Getfollowing(userid);
            var result = from f in followings
                    join post in _context.Posts on f.Following_AccountId equals post.AccountId
                    join acc in _context.Accounts on f.Following_AccountId equals acc.Id
                    where post.Status == 1 && acc.Status == 1
                    select new
                    {
                        AccountId = post.AccountId,
                        PostId = post.Id,
                        Name = acc.Name,
                        Text = post.Text,
                        Link = post.Video_link,
                        Date = post.Created_date
                    };
            var likes = from pl in _context.Post_Likes
                        join ac in _context.Accounts on pl.AccountId equals ac.Id
                        group pl by pl.Post_id into g
                        select new {
                            Post = g.Key,
                            Likes = g.Count()
                        };

            var res = from r in result
                    join like in likes on r.PostId equals like.Post into Details
                    from m in Details.DefaultIfEmpty()
                    select new
                    {
                        AccountId = r.AccountId,
                        PostId = r.PostId,
                        Name = r.Name,
                        Text = r.Text,
                        Link = r.Link,
                        Date = r.Date,
                        Like = m?.Likes ?? 0
                    };
            res = res.OrderByDescending(o => o.Date);
            return Ok(res);
        }

        [HttpDelete("{id}/deletepost/{postid}")]
        public async Task<IActionResult> DeletePost(int id, int postid)
        {
            /*if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();*/

            var postFromRepo = await _repo.GetPost(postid);
            if(postFromRepo != null)
            {
                 _repo.Delete(postFromRepo);
            }
            if(await _repo.SaveAll()) {
                return Ok();
            }
            return BadRequest();
             
        }

        [HttpPut("{id}/update/{postid}")]
        public async Task<IActionResult> UpdatePost(int id, int postid, PostForUpdateDto postForUpdateDto)
        {
            /*if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();*/
            postForUpdateDto.Video_link = postForUpdateDto.Video_link.Replace("watch?v=","embed/");
            var postFromRepo = await _repo.GetPost(postid);

            _mapper.Map(postForUpdateDto, postFromRepo);

            if(await _repo.SaveAll())
            {
                return Ok(postFromRepo);
            }
               
            
            throw new Exception($"Updating user {id} failed on save");
        }

        [HttpGet("search/{word}")]
        public async Task<IActionResult> GetSearch(string word)
        {
           
            var parameters = new Dictionary<string, string>
            {
                ["key"] = "AIzaSyAOlmjRIUBMpdKQcHTkAeaGaX7DO1KQYQE",
                ["part"] = "snippet",
                ["q"] = word,
                ["maxResults"] = "7"
                
            };

            var baseUrl = "https://www.googleapis.com/youtube/v3/search?";
            var fullUrl = MakeUrlWithQuery(baseUrl, parameters);

            dynamic result = await new HttpClient().GetStringAsync(fullUrl);
            result = JsonConvert.DeserializeObject(result);
            List<SearchToReturn> st = new List<SearchToReturn>();
            if (result != null)
            {
                foreach (dynamic item in result.items)
                {
                    /*st.Title = item.snippet.title;
                    st.Description = item.snippet.description;
                    st.Link = item.snippet.resourceId.videoId;*/
                    st.Add(new SearchToReturn { Title = item.snippet.title,
                                                Description = item.snippet.description,
                                                Link = "https://www.youtube.com/embed/"+item.id.videoId});
                }
                return Ok(st);
            }

            return default(dynamic);
        }

        [HttpGet("playlist/{link}")]
        public async Task<IActionResult> GetPlayList(string link)
        {
           var parameters = new Dictionary<string, string>
            {
                ["key"] = "AIzaSyAOlmjRIUBMpdKQcHTkAeaGaX7DO1KQYQE",
                ["playlistId"] = link,
                ["part"] = "snippet",
                ["fields"] = "items/snippet(title, description, resourceId)",
                ["maxResults"] = "50"
            };

           

            var baseUrl = "https://www.googleapis.com/youtube/v3/playlistItems?";
            var fullUrl = MakeUrlWithQuery(baseUrl, parameters);

            dynamic result = await new HttpClient().GetStringAsync(fullUrl);
            result = JsonConvert.DeserializeObject(result);
            List<SearchToReturn> st = new List<SearchToReturn>();
            if (result != null)
            {
                foreach (dynamic item in result.items)
                {
                    /*st.Title = item.snippet.title;
                    st.Description = item.snippet.description;
                    st.Link = item.snippet.resourceId.videoId;*/
                    st.Add(new SearchToReturn { Title = item.snippet.title,
                                                Description = item.snippet.description,
                                                Link = "https://www.youtube.com/embed/" + item.snippet.resourceId.videoId});
                }
                return Ok(st);
                 
            }

            return default(dynamic);
        }

         private static string MakeUrlWithQuery(string baseUrl, 
            IEnumerable<KeyValuePair<string, string>> parameters)
        {
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException(nameof(baseUrl));

            if (parameters == null || parameters.Count() == 0)
                return baseUrl;

            return parameters.Aggregate(baseUrl,
                (accumulated, kvp) => string.Format($"{accumulated}{kvp.Key}={kvp.Value}&"));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _repo.GetPost(id);
            return Ok(post);
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var news = await _repo.GetPost(id);
            news.Status = 0;

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating news {id} failed on save");
        }

        [HttpGet("{id}/posts")]
        public async Task<IActionResult> GetUserPosts(int id)
        {
            var posts = await _repo.GetPosts();
            posts = posts.Where(i => i.AccountId==id);

            var result = from post in posts
                    join acc in _context.Accounts on post.AccountId equals acc.Id
                    select new
                    {
                        AccountId = post.AccountId,
                        PostId = post.Id,
                        Name = acc.Name,
                        Text = post.Text,
                        Link = post.Video_link,
                        Date = post.Created_date
                    };
            var likes = from pl in _context.Post_Likes
                        join ac in _context.Accounts on pl.AccountId equals ac.Id
                        group pl by pl.Post_id into g
                        select new {
                            Post = g.Key,
                            Likes = g.Count()
                        };

            var res = from r in result
                    join like in likes on r.PostId equals like.Post into Details
                    from m in Details.DefaultIfEmpty()
                    select new
                    {
                        AccountId = r.AccountId,
                        PostId = r.PostId,
                        Name = r.Name,
                        Text = r.Text,
                        Link = r.Link,
                        Date = r.Date,
                        Like = m?.Likes ?? 0
                    };
            
            res = res.OrderByDescending(i => i.Date);
            return Ok(res);

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

            var likes = from pl in _context.Post_Likes
                        join ac in _context.Accounts on pl.AccountId equals ac.Id
                        group pl by pl.Post_id into g
                        select new {
                            Post = g.Key,
                            Likes = g.Count()
                        };
            

            var result = from ap in account_percentage
                    join post in _context.Posts on ap.Id equals post.AccountId
                    join acc in _context.Accounts on post.AccountId equals acc.Id
                    where post.Created_date > dateCriteria && post.Status == 1 && acc.Status == 1
                    select new
                    {
                        AccountId = post.AccountId,
                        PostId = post.Id,
                        Name = acc.Name,
                        Text = post.Text,
                        Link = post.Video_link,
                        Date = post.Created_date
                    };

            var res = from r in result
                    join like in likes on r.PostId equals like.Post into Details
                    from m in Details.DefaultIfEmpty()
                    select new
                    {
                        AccountId = r.AccountId,
                        PostId = r.PostId,
                        Name = r.Name,
                        Text = r.Text,
                        Link = r.Link,
                        Date = r.Date,
                        Like = m?.Likes ?? 0
                    };
           /* foreach(var p in result)
            {
                var like = await _repo.GetLike(id,p.PostId);
                if(like != null ) p.Like = true;
            }*/
                
            return Ok(res);


 
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