using KnightFrank.AuthServer.Authentication;
using KnightFrank.AuthServer.Authorization.Common;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KnightFrank.AuthServer.Authorization.Requirement
{
    public class AssignRolesAuthorizationRequirement : IAuthorizationRequirement
    {

    }

    public class AssignRolesAuthorizationHandler : AuthorizationHandler<AssignRolesAuthorizationRequirement, (IEnumerable<string> newRoles, IEnumerable<string> currentRoles)>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AssignRolesAuthorizationRequirement requirement, (IEnumerable<string> newRoles, IEnumerable<string> currentRoles) roles)
        {
            if (!GetIsRolesChanged(roles.newRoles, roles.currentRoles))
            {
                context.Succeed(requirement);
            }
            else if (context.User.HasClaim(ClaimConstants.Permission, ApplicationPermissions.AssignRoles))
            {
                if (context.User.HasClaim(ClaimConstants.Permission, ApplicationPermissions.ViewRoles)) // If user has ViewRoles permission, then he can assign any roles
                    context.Succeed(requirement);

                else if (GetIsUserInAllAddedRoles(context.User, roles.newRoles, roles.currentRoles)) // Else user can only assign roles they're part of
                    context.Succeed(requirement);
            }


            return Task.CompletedTask;
        }

        private static bool GetIsRolesChanged(IEnumerable<string> newRoles, IEnumerable<string> currentRoles)
        {
            if (newRoles == null)
                newRoles = new List<string>();

            if (currentRoles == null)
                currentRoles = new List<string>();

            bool roleAdded = newRoles.Except(currentRoles).Any();
            bool roleRemoved = currentRoles.Except(newRoles).Any();

            return roleAdded || roleRemoved;
        }

        private static bool GetIsUserInAllAddedRoles(ClaimsPrincipal contextUser, IEnumerable<string> newRoles, IEnumerable<string> currentRoles)
        {
            if (newRoles == null)
                newRoles = new List<string>();

            if (currentRoles == null)
                currentRoles = new List<string>();


            var addedRoles = newRoles.Except(currentRoles);

            return addedRoles.All(role => contextUser.IsInRole(role));
        }
    }
}
