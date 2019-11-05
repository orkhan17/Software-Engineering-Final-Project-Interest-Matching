using System.Collections.Generic;
using System.Threading.Tasks;
using Project.API.Models;

namespace Project.API.Data.IRepository
{
    public interface IAccountRepository
    {
         void Add<T>(T entity) where T:class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<IEnumerable<Account>> GetAccounts();
         Task<Account> GetAccount(int id);
    }
}