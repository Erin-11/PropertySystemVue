using KnightFrank.AuthServer.DAL.Entities.Models;
using KnightFrank.DataAccessLayer.EF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KnightFrank.AuthServer.DAL
{
    public interface IAccountManager
    {
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<string>> GetUserRolesAsync(ApplicationUser user);
        Task<(ApplicationUser User, IEnumerable<string> Roles)?> GetUserAndRolesAsync(string userId);
        Task<(IEnumerable<(ApplicationUser User, IEnumerable<string> Roles)>, int TotalCount)> GetUsersAndRolesAsync(Page page, Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> orderBy, Expression<Func<ApplicationUser, bool>> filter);
        Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(string userid, ApplicationUser user, IEnumerable<string> roles, string password);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(string userid, ApplicationUser user);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateUserAsync(string userid, ApplicationUser user, IEnumerable<string> roles);
        Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(string userid, string userId);
        Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteUserAsync(string userid, ApplicationUser user);
        Task<(bool Succeeded, IEnumerable<string> Errors)> ResetPasswordAsync(ApplicationUser user, string newPassword);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdatePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        Task<ApplicationRole> GetRoleByIdAsync(string roleId);
        Task<ApplicationRole> GetRoleByNameAsync(string roleName);
        Task<ApplicationRole> GetRoleLoadRelatedAsync(string roleName);
        Task<IEnumerable<ApplicationRole>> GetRolesAsync();
        Task<IEnumerable<ApplicationRole>> GetRolesLoadRelatedAsync();
        Task<(IEnumerable<ApplicationRole> Roles, int TotalCount)> GetRolesLoadRelatedAsync(Page page, Func<IQueryable<ApplicationRole>, IOrderedQueryable<ApplicationRole>> orderBy, Expression<Func<ApplicationRole, bool>> filter);
        Task<(bool Succeeded, IEnumerable<string> Errors)> CreateRoleAsync(string userid, ApplicationRole role, IEnumerable<string> claims);
        Task<(bool Succeeded, IEnumerable<string> Errors)> UpdateRoleAsync(string userid, ApplicationRole role, IEnumerable<string> claims);
        Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteRoleAsync(string userid, string roleName);
        Task<(bool Succeeded, IEnumerable<string> Errors)> DeleteRoleAsync(string userid, ApplicationRole role);
    }
}
