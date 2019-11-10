using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ActivityAcme.API.Domain.Models;
using ActivityAcme.API.Domain.Repositories;
using ActivityAcme.API.Persistence.Contexts;

namespace ActivityAcme.API.Persistence.Repositories
{
    public class ActivityRepository : BaseRepository, IActivityRepository
    {
        public ActivityRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Activity>> ListAsync()
        {
            return await _context.Activitys
                                 .AsNoTracking()
                                 .ToListAsync();

            // AsNoTracking tells EF Core it doesn't need to track changes on listed entities. Disabling entity
            // tracking makes the code a little faster
        }

        public async Task AddAsync(Activity activity)
        {
            await _context.Activitys.AddAsync(activity);
        }

        public async Task<Activity> FindByIdAsync(int id)
        {
            return await _context.Activitys.FindAsync(id);
        }

        public void Update(Activity activity)
        {
            _context.Activitys.Update(activity);
        }

        public void Remove(Activity activity)
        {
            _context.Activitys.Remove(activity);
        }
    }
}