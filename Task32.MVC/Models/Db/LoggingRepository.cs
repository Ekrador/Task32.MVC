using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Task32.MVC.Models.Db;

namespace Task32.MVC.Models.Db
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly BlogContext _context;
        public LoggingRepository(BlogContext context)
        {
            _context = context;
        }

        public async Task<Request[]> GetRequests()
        {
            return await _context.Requests.ToArrayAsync();
        }

        public async Task Log(Request request)
        {
            request.Id = Guid.NewGuid();
            request.Date = DateTime.Now;
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }
    }
}
