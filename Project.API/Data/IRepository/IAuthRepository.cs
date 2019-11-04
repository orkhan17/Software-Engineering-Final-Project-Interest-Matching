using System.Threading.Tasks;
using Project.API.Models;

namespace Project.API.Data.IRepository
{
    public interface IAuthRepository
    {
         Task<Account> Register(Account account, string password);
         Task<Account> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}