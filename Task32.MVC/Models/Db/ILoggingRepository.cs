using System.Threading.Tasks;

namespace Task32.MVC.Models.Db
{
    public interface ILoggingRepository
    {
        Task Log(Request request);
        Task<Request[]> GetRequests();
    }
}
