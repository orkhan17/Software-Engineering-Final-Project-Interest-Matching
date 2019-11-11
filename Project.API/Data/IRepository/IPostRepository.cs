using System.Collections.Generic;
using System.Threading.Tasks;
using Project.API.Models;

namespace Project.API.Data.IRepository
{
    public interface IPostRepository
    {
         void Add<T>(T entity) where T:class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<IEnumerable<Post>> GetPosts();
         Task<Post> GetPost(int id);
         Task<IEnumerable<Visited_profile>> GetVisitedProfiles(int id);
         Task<IEnumerable<Account>> GetAccounts();
         Task<IEnumerable<Music_type_account>> GetAccountsPreference(int id);

    }
}