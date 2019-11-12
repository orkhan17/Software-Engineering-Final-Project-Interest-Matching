using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.API.Data.IRepository;
using Project.API.Models;

namespace Project.API.Data.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        public PostRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<IEnumerable<Music_type_account>> GetAccountsPreference(int id)
        {
            var pref = await _context.Music_type_accounts.Where(i => i.Account_Id == id).ToListAsync();
            return pref;
        }

        public async Task<Post> GetPost(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<IEnumerable<Visited_profile>> GetVisitedProfiles(int id)
        {
            var profiles =  _context.Visited_profiles.AsQueryable();
            var visited_profile = profiles.Where(p => p.AccountId == id).ToListAsync();
            return await visited_profile;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Account> GetAccount(int id)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            return user;
        }

        public async Task<Post_Like> GetLike(int userid, int postid)
        {
            var like = await _context.Post_Likes.FirstOrDefaultAsync( l => l.AccountId==userid && l.Post_id==postid);
            return like;
        }
    }
}