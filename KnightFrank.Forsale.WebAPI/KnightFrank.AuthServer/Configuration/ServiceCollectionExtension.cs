using IdentityServer4.Models;
using KnightFrank.AuthServer.DAL;
using KnightFrank.AuthServer.DAL.Core;
using KnightFrank.AuthServer.DAL.Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;

namespace KnightFrank.AuthServer.Configuration
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, bool isDevelopment, string signingCredential = null, string validationKey = null, bool includDB = false,
            IEnumerable<ApiScope> apiScopes = null, IEnumerable<ApiResource> apiResources = null, IEnumerable<Client> clients = null)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowCredentials()
                            //.WithOrigins(
                            //    "https://localhost:44311",
                            //    "https://localhost:44352",
                            //    "https://localhost:44372",
                            //    "https://localhost:44378",
                            //    "https://localhost:44390")
                            //.SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            //services.AddAuthentication(o =>
            //{
            //    o.DefaultScheme = IdentityConstants.ApplicationScheme;
            //    o.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            //})
            //.AddIdentityCookies(o => { });

            //services.AddAuthorization();

            var idsvrBuilder = services.AddIdentityServer(options =>
            {
                options.Authentication.CheckSessionCookieName = "_kf";
                options.Authentication.CookieSlidingExpiration = true;
                options.Authentication.CookieLifetime = new TimeSpan(365, 0, 0, 0);
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryPersistedGrants()
            .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
            .AddInMemoryApiScopes(apiScopes ?? IdentityServerConfig.GetApiScopes())
            .AddInMemoryApiResources(apiResources ?? IdentityServerConfig.GetApiResources())
            .AddInMemoryClients(clients ?? IdentityServerConfig.GetClients());

            if (includDB)
            {
                idsvrBuilder.AddAspNetIdentity<ApplicationUser>()
                    .AddProfileService<ProfileService>();
            }


            if (isDevelopment)
            {
                idsvrBuilder.AddDeveloperSigningCredential();
                //services.AddIdentityServer(options =>
                //{
                //    options.Authentication.CheckSessionCookieName = "_pss";
                //    options.Authentication.CookieSlidingExpiration = true;
                //    options.Authentication.CookieLifetime = new TimeSpan(24, 0, 0);
                //})
                //// The AddDeveloperSigningCredential extension creates temporary key material for signing tokens.
                //// This might be useful to get started, but needs to be replaced by some persistent key material for production scenarios.
                //// See http://docs.identityserver.io/en/release/topics/crypto.html#refcrypto for more information.
                //.AddDeveloperSigningCredential()
                //.AddInMemoryPersistedGrants()
                //// To configure IdentityServer to use EntityFramework (EF) as the storage mechanism for configuration data (rather than using the in-memory implementations),
                //// see https://identityserver4.readthedocs.io/en/release/quickstarts/8_entity_framework.html
                //.AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                //.AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                //.AddInMemoryClients(IdentityServerConfig.GetClients())
                //.AddAspNetIdentity<ApplicationUser>()
                //.AddProfileService<ProfileService>();
            }
            else
            {
                idsvrBuilder.AddSigningCredential(X509.GetCertificate(signingCredential)).AddValidationKey(X509.GetCertificate(validationKey));
                //services.AddIdentityServer(options =>
                //{
                //    options.Authentication.CookieSlidingExpiration = true;
                //    options.Authentication.CookieLifetime = new TimeSpan(365, 0, 0, 0);
                //})
                ////The AddDeveloperSigningCredential extension creates temporary key material for signing tokens.
                ////This might be useful to get started, but needs to be replaced by some persistent key material for production scenarios.
                ////See http://docs.identityserver.io/en/release/topics/crypto.html#refcrypto for more information.
                ////.AddSigningCredential(X509.GetCertificate("FED7A7C45A1F6990ACDF23B1FBF5E5C0E4D1B157"))
                ////.AddValidationKey(X509.GetCertificate("492AC7E13CA2632E01C0445E2907AB9423694A4E"))
                //.AddSigningCredential(X509.GetCertificate(signingCredential))
                //.AddValidationKey(X509.GetCertificate(validationKey))
                //.AddInMemoryPersistedGrants()
                ////To configure IdentityServer to use EntityFramework(EF) as the storage mechanism for configuration data (rather than using the in-memory implementations),
                ////see https://identityserver4.readthedocs.io/en/release/quickstarts/8_entity_framework.html
                //.AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                //.AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                //.AddInMemoryClients(IdentityServerConfig.GetClients())
                //.AddAspNetIdentity<ApplicationUser>()
                //.AddProfileService<ProfileService>();
            }


            return services;
        }

        public static IServiceCollection AddApplicationIdentityDbContext(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                //.AddRoles<ApplicationRole>()
                //.AddEntityFrameworkStores<DBEntities>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opts =>
            {
                // User settings
                opts.User.RequireUniqueEmail = true;

                // Password settings
                opts.Password.RequireDigit = true;
                opts.Password.RequiredLength = 8;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireLowercase = false;

                // Lockout settings
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                opts.Lockout.MaxFailedAccessAttempts = 10;
            });


            services.AddScoped<IAccountManager, AccountManager>();

            return services;
        }
    }

    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseApplicationIdentity(this IApplicationBuilder application)
        {
            application.UseIdentityServer();
            //application.UseAuthentication();
            //application.UseAuthorization();


            return application;
        }
    }
}
