using KnightFrank.AuthServer.Authentication;
using KnightFrank.AuthServer.Authorization.Common;
using KnightFrank.AuthServer.DAL.Entities.Models;
using KnightFrank.DataAccessLayer.EF;
using KnightFrank.DataAccessLayer.EF.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KnightFrank.AuthServer.DAL.Core
{
    public class AccountManager : IAccountManager
    {
        private readonly IDataContextAsync _dataContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountManager(IDataContextAsync dataContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<(ApplicationUser User, IEnumerable<string> Roles)?> GetUserAndRolesAsync(string userId)
        {
            //var user = await _context.Users
            //    .Include(u => u.Roles)
            //    .Where(u => u.Id == userId)
            //    .SingleOrDefaultAsync();

            //var userRoleIds = user.UserRoles.Select(r => r.RoleId).ToList();

            //var roles = await _context.Roles
            //    .Where(r => userRoleIds.Contains(r.Id))
            //    .Select(r => r.Name)
            //    .ToArrayAsync();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return (user, roles);
        }

        public async Task<(IEnumerable<(ApplicationUser User, IEnumerable<string> Roles)>, int TotalCount)> GetUsersAndRolesAsync(Page page, Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> orderBy, Expression<Func<ApplicationUser, bool>> filter)
        {
            Expression<Func<ApplicationUser, bool>> expression = c => c.IsActive;

            if (filter != null)
            {
                expression = expression.And(filter);
            }

            var usersQuery = _userManager.Users.Include(c => c.UserRoles).Where(expression);

            var totalCount = usersQuery.Count();

            if (orderBy != null)
            {
                usersQuery = orderBy(usersQuery);
            }

            var users = usersQuery.Skip(page.Skip).Take(page.PageSize);

            var userRoleIds = users.SelectMany(u => u.UserRoles.Select(r => r.RoleId)).ToList();

            var roles = await _roleManager.Roles.Where(r => userRoleIds.Contains(r.Id)).ToArrayAsync();

            var result = users.AsEnumerable().Select(u => (u, roles.Where(r => u.UserRoles.Select(ur => ur.RoleId).Contains(r.Id)).Select(r => r.Name)));

            return (result, totalCount);
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(string userid, ApplicationUser user, IEnumerable<string> roles, string password)
        {
            user.CreatedBy = userid;
            user.CreatedDate = DateTime.Now;
            user.IsActive = true;
            user.ObjectState = ObjectState.Added;
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            user.ObjectState = ObjectState.Unchanged;
            user = await _userManager.FindByNameAsync(user.UserName);

            try
            {
                result = await _userManager.AddToRolesAsync(user, roles.Distinct());
            }
            catch
            {
                await DeleteUserAsync(userid, user);
                throw;
            }

            if (!result.Succeeded)
            {
                await DeleteUserAsync(userid, user);
                return (false, result.Errors.Select(e => e.Description));
            }

            return (true, new List<string>());
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(string userid, ApplicationUser user)
        {
            return await UpdateUserAsync(userid, user, null);
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(string userid, ApplicationUser user, IEnumerable<string> roles)
        {
            user.ModifiedBy = userid;
            user.ModifiedDate = DateTime.Now;
            user.IsActive = true;
            user.ObjectState = ObjectState.Modified;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            if (roles != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var rolesToRemove = userRoles.Except(roles).ToArray();
                var rolesToAdd = roles.Except(userRoles).Distinct();

                if (rolesToRemove.Any())
                {
                    result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                    if (!result.Succeeded)
                        return (false, result.Errors.Select(e => e.Description));
                }

                if (rolesToAdd.Any())
                {
                    result = await _userManager.AddToRolesAsync(user, rolesToAdd);
                    if (!result.Succeeded)
                        return (false, result.Errors.Select(e => e.Description));
                }
            }

            return (true, new List<string>());
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(string userid, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
                return await DeleteUserAsync(userid, user);

            return (true, new List<string>());
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(string userid, ApplicationUser user)
        {
            user.ObjectState = ObjectState.Deleted;
            var result = await _userManager.DeleteAsync(user);
            return (result.Succeeded, result.Errors.Select(e => e.Description));
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> ResetPasswordAsync(ApplicationUser user, string newPassword)
        {
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            return (true, new List<string>());
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdatePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToArray());

            return (true, new List<string>());
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            var _user = await _userManager.FindByIdAsync(user.Id.ToString());
            if (!await _userManager.CheckPasswordAsync(_user, password))
            {
                if (!_userManager.SupportsUserLockout)
                    await _userManager.AccessFailedAsync(user);

                return false;
            }

            return true;
        }


        public async Task<ApplicationRole> GetRoleByIdAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task<ApplicationRole> GetRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<ApplicationRole> GetRoleLoadRelatedAsync(string roleName)
        {
            //var role = await _context.Roles
            //    .Include(r => r.Claims)
            //    .Include(r => r.Users)
            //    .Where(r => r.Name == roleName)
            //    .SingleOrDefaultAsync();

            var role = await _roleManager.FindByNameAsync(roleName);

            return role;
        }

        public async Task<IEnumerable<ApplicationRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IEnumerable<ApplicationRole>> GetRolesLoadRelatedAsync()
        {
            return await _roleManager.Roles.Include(c => c.RoleClaims).ToListAsync();
        }

        public async Task<(IEnumerable<ApplicationRole> Roles, int TotalCount)> GetRolesLoadRelatedAsync(Page page, Func<IQueryable<ApplicationRole>, IOrderedQueryable<ApplicationRole>> orderBy, Expression<Func<ApplicationRole, bool>> filter)
        {
            Expression<Func<ApplicationRole, bool>> expression = c => c.IsActive;

            if (filter != null)
            {
                expression = expression.And(filter);
            }

            var rolesQuery = _roleManager.Roles.Include(c => c.RoleClaims).Where(expression);

            var totalCount = rolesQuery.Count();

            if (orderBy != null)
            {
                rolesQuery = orderBy(rolesQuery);
            }

            var roles = rolesQuery.Skip(page.Skip).Take(page.PageSize);

            return await Task.FromResult((roles, totalCount));
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> CreateRoleAsync(string userid, ApplicationRole role, IEnumerable<string> claims)
        {
            if (claims == null)
                claims = new List<string>();

            var invalidClaims = claims.Where(c => ApplicationPermissions.GetPermissionByValue(c) == null);
            if (invalidClaims.Any())
                return (false, new[] { "The following claim types are invalid: " + string.Join(", ", invalidClaims) });

            role.CreatedBy = userid;
            role.CreatedDate = DateTime.Now;
            role.IsActive = true;
            role.ObjectState = ObjectState.Added;
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            role.ObjectState = ObjectState.Unchanged;
            role = await _roleManager.FindByNameAsync(role.Name);

            foreach (string claim in claims.Distinct())
            {
                result = await _roleManager.AddClaimAsync(role, new Claim(ClaimConstants.Permission, ApplicationPermissions.GetPermissionByValue(claim)));

                if (!result.Succeeded)
                {
                    await DeleteRoleAsync(userid, role);
                    return (false, result.Errors.Select(e => e.Description));
                }
            }

            return (true, new List<string>());
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateRoleAsync(string userid, ApplicationRole role, IEnumerable<string> claims)
        {
            if (claims != null)
            {
                var invalidClaims = claims.Where(c => ApplicationPermissions.GetPermissionByValue(c) == null);
                if (invalidClaims.Any())
                    return (false, new[] { "The following claim types are invalid: " + string.Join(", ", invalidClaims) });
            }

            role.ObjectState = ObjectState.Modified;
            role.IsActive = true;
            role.ModifiedBy = userid;
            role.ModifiedDate = DateTime.Now;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description));

            if (claims != null)
            {
                var roleClaims = (await _roleManager.GetClaimsAsync(role)).Where(c => c.Type == ClaimConstants.Permission);
                var roleClaimValues = roleClaims.Select(c => c.Value);

                var claimsToRemove = roleClaimValues.Except(claims);
                var claimsToAdd = claims.Except(roleClaimValues).Distinct();

                if (claimsToRemove.Any())
                {
                    foreach (string claim in claimsToRemove)
                    {
                        result = await _roleManager.RemoveClaimAsync(role, roleClaims.Where(c => c.Value == claim).FirstOrDefault());
                        if (!result.Succeeded)
                            return (false, result.Errors.Select(e => e.Description));
                    }
                }

                if (claimsToAdd.Any())
                {
                    foreach (string claim in claimsToAdd)
                    {
                        result = await _roleManager.AddClaimAsync(role, new Claim(ClaimConstants.Permission, ApplicationPermissions.GetPermissionByValue(claim)));
                        if (!result.Succeeded)
                            return (false, result.Errors.Select(e => e.Description));
                    }
                }
            }

            return (true, new List<string>());
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteRoleAsync(string userid, string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null)
                return await DeleteRoleAsync(userid, role);

            return (true, new List<string>());
        }

        public async Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteRoleAsync(string userid, ApplicationRole role)
        {
            role.ObjectState = ObjectState.Deleted;
            var result = await _roleManager.DeleteAsync(role);
            return (result.Succeeded, result.Errors.Select(e => e.Description));
        }
    }
}
