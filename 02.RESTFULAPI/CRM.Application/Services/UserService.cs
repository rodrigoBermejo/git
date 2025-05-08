using CRM.Application.Helpers;
using CRM.Core.Entities;
using CRM.Core.Interfaces;
using CRM.Infraestructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.Versioning;
using System.Security.Claims;

namespace CRM.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;
        private readonly LdapService _ldapService;

        public UserService(IHttpContextAccessor httpContextAccessor, AppDbContext context, LdapService ldapService)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _ldapService = ldapService;
        }

        public Guid GetCurrentUserId()
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                return user.IdUser;
            }
            else {
                return Guid.Empty;
            }
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();
            return userId != Guid.Empty ? await _context.Users.FindAsync(userId) : null;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.OrderBy(u => u.Created).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.IdUser == id);
        }

        public async Task AddUserAsync(User user)
        {
            var newUser = user;
            newUser.Created = DateTime.UtcNow;
            newUser.CreatedById = GetCurrentUserId();
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.IdUser);
            if (existingUser != null)
            {
                existingUser = user;
                existingUser.Updated = DateTime.UtcNow;
                existingUser.UpdatedById = GetCurrentUserId();
                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("User not found");
            }
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.IdUser == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        [SupportedOSPlatform("windows")]
        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            try
            {
                bool isValid = _ldapService.ValidateUser(username, password);
                if (isValid)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
                    if (user != null)
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Unexpected error occurred while validating user {0}", username),ex);
            }
        }
    }
}
