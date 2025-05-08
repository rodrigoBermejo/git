using CRM.Core.Entities;
using CRM.Core.Interfaces;
using CRM.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public ActivityRepository(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<Activity>> GetAllAsync()
        {
            return await _context.Activities.OrderBy(a => a.Created).ToListAsync();
        }

        public async Task<Activity?> GetByIdAsync(Guid id)
        {
            return await _context.Activities.FindAsync(id) ?? throw new InvalidOperationException("Activity not found");
        }

        public async Task AddAsync(Activity activity)
        {
            var newActivity = activity;
            newActivity.Created = DateTime.UtcNow;
            newActivity.CreatedById = _userService.GetCurrentUserId();
            _context.Activities.Add(newActivity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Activity activity)
        {
            var existingActivity = await _context.Activities.FindAsync(activity.IdActivity);
            if (existingActivity != null)
            {
                existingActivity = activity;
                existingActivity.Updated = DateTime.UtcNow;
                existingActivity.UpdatedById = _userService.GetCurrentUserId();

                _context.Activities.Update(existingActivity);
                await _context.SaveChangesAsync();
            }
            else {
                throw new InvalidOperationException("Activity not found");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity != null)
            {
                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Activity not found");
            }
        }

        public async Task<IEnumerable<Activity>> GetActivitiesUpdatedByUserAsync(Guid userId)
        {
            return await _context.Activities
                .Where(a => a.UpdatedById == userId)
                .ToListAsync();
        }

    }
}
