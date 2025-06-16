using Microsoft.AspNetCore.Builder;

namespace KnightFrank.MemfusWongData.Api.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void InitializeIdentityDatabase(this IApplicationBuilder app)
        {
            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            //    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<DbInitializer>>();

            //    DbInitializer.CreateDefaultUserAndRoleForApplication(userManager, roleManager, logger).Wait();
            //}
        }
    }
}
