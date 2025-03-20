using Database.Models;
using System.Threading.Tasks;

namespace Database.Repos
{
    public interface IUserRepo
    {
        Task<bool> CreateAsync(ApplicationUser user);
        Task<ApplicationUser> FindByNameAsync(string email);
        //void UserAddress(User user);
    }
}
