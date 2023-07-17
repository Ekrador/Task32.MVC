using System.Threading.Tasks;

namespace Task32.MVC.Models.Db
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
    }
}
