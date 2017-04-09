using BlogTutorial.Data;
using BlogTutorial.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace BlogTutorial.Identity
{
    public class ApplicationUserStore : IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>, IQueryableUserStore<ApplicationUser>
    {
        private readonly BlogDbContext _context;

        public ApplicationUserStore(BlogDbContext dbContext)
        {
            _context = dbContext;
        }

        public IQueryable<ApplicationUser> Users => _context.Users;

        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var role = await _context.Roles.SingleAsync(r => r.RoleName == roleName);
            var userRole = new UserRole { User = user, Role = role };
            _context.UserRoles.Add(userRole);

            _context.SaveChanges();
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var dbUser = await _context.Users.SingleOrDefaultAsync(u => u.UserId == user.UserId);
            if (dbUser != null)
            {
                _context.Users.Remove(dbUser);
            }

            return IdentityResult.Success;
        }

        public void Dispose()
        {
            
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var id = Guid.Parse(userId);
            var user = await _context.Users.SingleAsync(u => u.UserId == id);

            return user;
        }

        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName.ToUpper() == normalizedUserName);
            return user;
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName.ToUpper());
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var roles = from r in _context.Roles
                        join ur in _context.UserRoles on r.RoleId equals ur.RoleId
                        where ur.UserId == user.UserId
                        select r.RoleName;

            return await roles.ToListAsync();
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserId.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public async Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var roles = from r in _context.Roles
                        join ur in _context.UserRoles on r.RoleId equals ur.RoleId
                        join u in _context.Users on ur.UserId equals u.UserId
                        where r.RoleName == roleName
                        select u;

            return await roles.ToListAsync();
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var roles = from r in _context.Roles
                        join ur in _context.UserRoles on r.RoleId equals ur.RoleId
                        where r.RoleName == roleName && ur.UserId==user.UserId
                        select r;

            return await roles.AnyAsync();
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var userRole = await (from ur in _context.UserRoles
                            join r in _context.Roles on ur.RoleId equals r.RoleId
                            where ur.UserId == user.UserId && r.RoleName == roleName
                            select ur).SingleOrDefaultAsync();

            _context.UserRoles.Remove(userRole);
            await _context.SaveChangesAsync();
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public async Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            await _context.SaveChangesAsync();
        }

        public async Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            await _context.SaveChangesAsync();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }
}
