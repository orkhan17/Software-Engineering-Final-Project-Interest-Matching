using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.API.Data.IRepository;
using Project.API.Models;

namespace Project.API.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        public AccountRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<Music_type_account> AccountMusic(int userid, int musicid)
        {
            var account_music = await _context.Music_type_accounts.FirstOrDefaultAsync(a => a.Account_Id == userid && a.Music_type_id == musicid);
            return account_music;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Account> GetAccount(int id)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            return user;
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<IEnumerable<Music_type>> GetMusicType()
        {
            return await _context.Music_types.ToListAsync();
        }

        public async Task<Music_type> GetMusicType(int id)
        {
            var music = await _context.Music_types.FirstOrDefaultAsync(a => a.Id == id);
            return music;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}