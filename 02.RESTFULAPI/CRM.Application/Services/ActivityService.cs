using CRM.Core.Entities;
using CRM.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM.Application.Services
{
    public class ActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUserService _userService;

        public ActivityService(IActivityRepository activityRepository, IUserService userService)
        {
            _activityRepository = activityRepository;
            _userService = userService;
        }

        public async Task<IEnumerable<Activity>> GetAllActivitiesAsync()
        {
            return await _activityRepository.GetAllAsync();
        }

        public async Task<Activity?> GetActivityByIdAsync(Guid id)
        {
            return await _activityRepository.GetByIdAsync(id);
        }

        public async Task AddActivityAsync(Activity activity)
        {
            activity.Created = DateTime.UtcNow;
            activity.CreatedById = _userService.GetCurrentUserId();
            await _activityRepository.AddAsync(activity);
        }

        public async Task UpdateActivityAsync(Activity activity)
        {
            activity.Updated = DateTime.UtcNow;
            activity.UpdatedById = _userService.GetCurrentUserId();
            await _activityRepository.UpdateAsync(activity);
        }

        public async Task DeleteActivityAsync(Guid id)
        {
            await _activityRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Activity>> GetActivitiesUpdatedByUser(Guid userId)
        {
            return await _activityRepository.GetActivitiesUpdatedByUserAsync(userId);
        }
    }
}