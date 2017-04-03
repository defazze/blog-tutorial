using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using BlogTutorial.Data.Models;
using System.Threading;
using System.Threading.Tasks;
using BlogTutorial.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogTutorial.Identity
{
    public class ApplicationRoleStore : IRoleStore<Role>
    {
        private BlogDbContext _context;

        public ApplicationRoleStore(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
        {
            var dbRole = await _context.Roles.SingleAsync(r => r.RoleId == role.RoleId);
            _context.Roles.Remove(dbRole);

            await _context.SaveChangesAsync();

            return IdentityResult.Success;
        }

        public void Dispose()
        {

        }

        public async Task<Role> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var id = Guid.Parse(roleId);

            return await _context.Roles.SingleAsync(r => r.RoleId == id);
        }

        public async Task<Role> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await _context.Roles.SingleOrDefaultAsync(r => r.RoleName == normalizedRoleName);
        }

        public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.RoleName);
        }

        public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.RoleId.ToString());
        }

        public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.RoleName);
        }

        public  Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public async Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
        {
            role.RoleName = roleName;
            await _context.SaveChangesAsync();
        }

        public Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
