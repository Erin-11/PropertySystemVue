namespace KnightFrank.MemfusWongData.Api.Extensions
{
    public class DbInitializer
    {
        //const string administratorRoleName = "System Admin Role";
        //const string administratorUserName = "administrator";
        //const string administratorPassword = "SystemAdmin@2588";

        //public static async Task CreateDefaultUserAndRoleForApplication(UserManager<ApplicationUser> um, RoleManager<ApplicationRole> rm, ILogger<DbInitializer> logger)
        //{
        //    IEnumerable<string> administratorRoleClaims = new List<string>() { "users.view", "users.manage", "roles.view", "roles.manage", "roles.assign" };

        //    var role = await CreateDefaultAdministratorRole(rm, logger);
        //    await SetClaimsToDefaultAdministratorRole(rm, logger, administratorRoleClaims, role);
        //    var user = await CreateDefaultUser(um, logger);
        //    //await SetPasswordForDefaultUser(um, logger, user);
        //    await AddDefaultRoleToDefaultUser(um, logger, administratorRoleName, user);
        //}

        //private static async Task<ApplicationRole> CreateDefaultAdministratorRole(RoleManager<ApplicationRole> rm, ILogger<DbInitializer> logger)
        //{
        //    logger.LogInformation($"Create the role `{administratorRoleName}` for application");
        //    var role = new ApplicationRole()
        //    {
        //        Name = administratorRoleName,
        //        Description = "System admin perform all the operations.",
        //        IsActive = true,
        //        ObjectState = ObjectState.Added
        //    };
        //    if (!await rm.RoleExistsAsync(administratorRoleName))
        //    {
        //        var ir = await rm.CreateAsync(role);
        //        if (ir.Succeeded)
        //        {
        //            role.ObjectState = ObjectState.Unchanged;
        //            logger.LogDebug($"Created the role `{administratorRoleName}` successfully");
        //        }
        //        else
        //        {
        //            var exception = new ApplicationException($"Default role `{administratorRoleName}` cannot be created");
        //            logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
        //            throw exception;
        //        }
        //        var createdRole = await rm.FindByNameAsync(administratorRoleName);
        //        return createdRole;
        //    }
        //    return null;
        //}

        //private static async Task<ApplicationUser> CreateDefaultUser(UserManager<ApplicationUser> um, ILogger<DbInitializer> logger)
        //{
        //    logger.LogInformation($"Create default user with username `{administratorUserName}` for application");
        //    var user = new ApplicationUser()
        //    {
        //        UserName = administratorUserName,
        //        Email = "kf.system@hk.knightfrank.com",
        //        LockoutEnabled = true,
        //        DisplayName = "System Administrator",
        //        Title = "System - Administrator",
        //        IsActive = true,
        //        ObjectState = ObjectState.Added
        //    };
        //    if (await um.FindByNameAsync(administratorUserName) == null)
        //    {
        //        var ir = await um.CreateAsync(user, administratorPassword);
        //        if (ir.Succeeded)
        //        {
        //            user.ObjectState = ObjectState.Unchanged;
        //            logger.LogDebug($"Created default user `{administratorUserName}` successfully");
        //        }
        //        else
        //        {
        //            var exception = new ApplicationException($"Default user `{administratorUserName}` cannot be created");
        //            logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
        //            throw exception;
        //        }
        //        var createdUser = await um.FindByNameAsync(administratorUserName);
        //        return createdUser;
        //    }
        //    return null;
        //}

        //private static async Task SetPasswordForDefaultUser(UserManager<ApplicationUser> um, ILogger<DbInitializer> logger, ApplicationUser user)
        //{
        //    logger.LogInformation($"Set password for default user `{administratorUserName}`");
        //    if (user != null)
        //    {
        //        var ir = await um.AddPasswordAsync(user, administratorPassword);
        //        if (ir.Succeeded)
        //        {
        //            logger.LogTrace($"Set password `{administratorPassword}` for default user `{administratorUserName}` successfully");
        //        }
        //        else
        //        {
        //            var exception = new ApplicationException($"Password for the user `{administratorUserName}` cannot be set");
        //            logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
        //            throw exception;
        //        }
        //    }
        //}

        //private static async Task SetClaimsToDefaultAdministratorRole(RoleManager<ApplicationRole> rm, ILogger<DbInitializer> logger, IEnumerable<string> claims, ApplicationRole role)
        //{
        //    logger.LogInformation($"Set claims for default administrator role `{administratorRoleName}`");
        //    if (role != null)
        //    {
        //        foreach (var claim in claims)
        //        {
        //            var ir = await rm.AddClaimAsync(role, new Claim(ClaimConstants.Permission, ApplicationPermissions.GetPermissionByValue(claim)));
        //            if (ir.Succeeded)
        //            {
        //                logger.LogTrace($"Set claim `{claim}` for default administrator role `{administratorRoleName}` successfully");
        //            }
        //            else
        //            {
        //                var exception = new ApplicationException($"The claim `{claim}` cannot be set for the role `{administratorRoleName}`");
        //                logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
        //                throw exception;
        //            }
        //        }
        //    }
        //}

        //private static async Task AddDefaultRoleToDefaultUser(UserManager<ApplicationUser> um, ILogger<DbInitializer> logger, string administratorRole, ApplicationUser user)
        //{
        //    logger.LogInformation($"Add default user `{administratorUserName}` to role '{administratorRole}'");
        //    if (user != null)
        //    {
        //        var ir = await um.AddToRoleAsync(user, administratorRole);
        //        if (ir.Succeeded)
        //        {
        //            logger.LogDebug($"Added the role '{administratorRole}' to default user `{administratorUserName}` successfully");
        //        }
        //        else
        //        {
        //            var exception = new ApplicationException($"The role `{administratorRole}` cannot be set for the user `{administratorUserName}`");
        //            logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(ir));
        //            throw exception;
        //        }
        //    }
        //}

        //private static string GetIdentiryErrorsInCommaSeperatedList(IdentityResult ir)
        //{
        //    string errors = null;
        //    foreach (var identityError in ir.Errors)
        //    {
        //        errors += identityError.Description;
        //        errors += ", ";
        //    }
        //    return errors;
        //}
    }
}
