using CRM.Core.Entities;

namespace CRM.Core.Interfaces
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> GetAllAsync();
        
        Task<Activity?> GetByIdAsync(Guid id);
        
        Task AddAsync(Activity activity);
        
        Task UpdateAsync(Activity activity);
        
        Task DeleteAsync(Guid id);
        
        Task<IEnumerable<Activity>> GetActivitiesUpdatedByUserAsync(Guid userId);
    }
}
