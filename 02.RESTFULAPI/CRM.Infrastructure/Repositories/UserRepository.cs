using CRM.Core.Entities;
using CRM.Core.Interfaces;
using CRM.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IUserService _userService;

        public UserRepository(AppDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id) ?? throw new InvalidOperationException("User not found");
        }

        public async Task AddAsync(User user)
        {
            var newUser = user;
            newUser.Created = DateTime.UtcNow;
            newUser.CreatedById = _userService.GetCurrentUserId();
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.IdUser);
            if (existingUser != null)
            {
                existingUser = user;
                existingUser.Updated = DateTime.UtcNow;
                existingUser.UpdatedById = _userService.GetCurrentUserId();
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("User not found");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("User not found");
            }
        }
    }
}
