﻿using CRM.Core.Entities;

namespace CRM.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        
        Task<User> GetByIdAsync(Guid id);
        
        Task AddAsync(User user);
        
        Task UpdateAsync(User user);
        
        Task DeleteAsync(Guid id);
    }
}
