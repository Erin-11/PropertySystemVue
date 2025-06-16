using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using KnightFrank.AuthServer;
using KnightFrank.AuthServer.Configuration;
using KnightFrank.BAL.Configuration;
using KnightFrank.DAL;
using KnightFrank.DAL.Configuration;
using KnightFrank.EmailGateway.Configuration;
using KnightFrank.MemfusWongData.Api.Common;
using KnightFrank.MemfusWongData.Api.Filters;
using KnightFrank.MemfusWongData.Api.Helpers;
using KnightFrank.MemfusWongData.Api.Helpers.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KnightFrank.MemfusWongData.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            Environment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var memfusWongDataDBConnectionString = Configuration.GetConnectionString("MemfusWongDataDB");
            var applicationUrl = Configuration["ApplicationUrl"].TrimEnd('/');
            var signingCredential = Configuration["SigningCredential"];
            var validationKey = Configuration["ValidationKey"];
            
            Configuration.GetSection("Format").Bind(AppConfigs.Format);
            Configuration.GetSection("IdentityServerSetting").Bind(AppConfigs.IdentityServerSetting);
            
            services.Configure<RazorPagesOptions>(opt => opt.RootDirectory = "/WebApi/Pages");

            var clients = AppConfigs.IdentityServerSetting.Clients?.Select(e => new Client()
            {
                ClientId = e.ClientId,
                ClientName = e.ClientName,
                ClientSecrets = {
                    new Secret(e.ClientSecret.Sha256())
                },
                AllowedGrantTypes = Utility.GetIdentityGrantTypes(e.AllowedGrantTypes),
                AllowAccessTokensViaBrowser = Utility.GetAllowAccessTokensViaBrowser(e.AllowAccessTokensViaBrowser),
                RequireClientSecret = Utility.GetRequireClientSecret(e.RequireClientSecret),
                AllowedScopes = Utility.GetAllowedScopes(e.AllowedScopes),
                AllowOfflineAccess = Utility.GetAllowOfflineAccess(e.AllowOfflineAccess),
                RefreshTokenExpiration = Utility.GetRefreshTokenExpiration(e.RefreshTokenExpiration),
                AccessTokenLifetime = Utility.GetAccessTokenLifetime(e.AccessTokenLifetime)
            });
            var apiResources = AppConfigs.IdentityServerSetting.ApiResources?.Select(e => new ApiResource(e.Name, e.FriendlyName)
            {
                Scopes = e.Scopes.ToList()
            });
            var apiScopes = AppConfigs.IdentityServerSetting.ApiScopes?.Select(e => new ApiScope(e.Name)
            {
                UserClaims = Utility.GetApiScopeUserClaims(e.UserClaims)
            });

            services.AddApplicationDbContext<MemfusWongDataEntities>(memfusWongDataDBConnectionString, assembly);
            services.AddApplicationDataRepository();
            services.AddApplicationService();
            //services.AddApplicationIdentityDbContext();
            services.AddApplicationIdentity(Environment.IsDevelopment(), signingCredential, validationKey, clients: clients, apiResources: apiResources, apiScopes: apiScopes);

            //services.AddTransient(s => s.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",                  
                    builder => builder
                        .AllowAnyMethod()
                        //.WithOrigins(clientUrl)
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddSignalR();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = applicationUrl;
                    options.SupportedTokens = SupportedTokens.Jwt;
                    options.RequireHttpsMetadata = false; // Note: Set to true in production
                    options.ApiName = IdentityServerConfig.ApiName;
                    options.LegacyAudienceValidation = true;
                });


            services.AddAuthorization(options =>
            {
                //options.AddPolicy(Policies.ViewAllAgentsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.viewAgents));

                //var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(IdentityServerAuthenticationDefaults.AuthenticationScheme);
                //defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                //options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

                //options.AddPolicy(Policies.ViewAllUsersPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewUsers));
                //options.AddPolicy(Policies.ManageAllUsersPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManageUsers));

                //options.AddPolicy(Policies.ViewAllRolesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewRoles));
                //options.AddPolicy(Policies.ViewRoleByRoleNamePolicy, policy => policy.Requirements.Add(new ViewRoleAuthorizationRequirement()));
                //options.AddPolicy(Policies.ManageAllRolesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManageRoles));

                //options.AddPolicy(Policies.AssignAllowedRolesPolicy, policy => policy.Requirements.Add(new AssignRolesAuthorizationRequirement()));

                //options.AddPolicy(Policies.ViewAllRoundMethodsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewRoundMethods));
                //options.AddPolicy(Policies.ManageAllRoundMethodsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManageRoundMethods));

                //options.AddPolicy(Policies.ViewAllPaymentMethodsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewPaymentMethods));
                //options.AddPolicy(Policies.ManageAllPaymentMethodsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManagePaymentMethods));

                //options.AddPolicy(Policies.ViewAllPriorityGroupsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewPriorityGroups));
                //options.AddPolicy(Policies.ManageAllPriorityGroupsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManagePriorityGroups));

                //options.AddPolicy(Policies.ViewAllAgenciesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewAgencies));
                //options.AddPolicy(Policies.ManageAllAgenciesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManageAgencies));

                //options.AddPolicy(Policies.ViewAllSolicitorsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewSolicitors));
                //options.AddPolicy(Policies.ManageAllSolicitorsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManageSolicitors));

                //options.AddPolicy(Policies.ViewAllPropertiesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewProperties));
                //options.AddPolicy(Policies.ManageAllPropertiesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManageProperties));

                //options.AddPolicy(Policies.ViewUnitConsumptionSummaryPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewUnitConsumptionSummary));

                //options.AddPolicy(Policies.ViewUnitSelectionSummaryPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewUnitSelectionSummary));

                //options.AddPolicy(Policies.ViewPropertySummaryPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewUnitSelectionSummary));

                //options.AddPolicy(Policies.ViewPriceListPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewPriceList));

                //options.AddPolicy(Policies.ViewAllPreliminaryAgreementsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewPreliminaryAgreements));
                //options.AddPolicy(Policies.ManageAllPreliminaryAgreementsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManagePreliminaryAgreements));

                //options.AddPolicy(Policies.ViewSalesRegistrationPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewSalesRegistration));

                ////options.AddPolicy(Policies.ViewSalesPriorityPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewSalesPriority));
                //options.AddPolicy(Policies.ViewBallotingPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewBallotings));
                //options.AddPolicy(Policies.ManageAllBallotingsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManageBallotings));

                //options.AddPolicy(Policies.ViewAllApplicationsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewApplications));
                //options.AddPolicy(Policies.ManageAllApplicationsPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManageApplications));

                //options.AddPolicy(Policies.ViewAllBatchesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ViewBatches));
                //options.AddPolicy(Policies.ManageAllBatchesPolicy, policy => policy.RequireClaim(ClaimConstants.Permission, ApplicationPermissions.ManageBatches));
            });

            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
                options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder(IdentityServerAuthenticationDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build()));
                options.Filters.Add(new GlobalActionFilter());
                options.Filters.Add(new GlobalExceptoinFilter());
            }).AddNewtonsoftJson();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KnightFrank.MemfusWongData.Api", Version = "v1" });

                c.AddSecurityDefinition(IdentityServerAuthenticationDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = IdentityServerAuthenticationDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = IdentityServerAuthenticationDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = IdentityServerAuthenticationDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });

            if (!Environment.IsDevelopment() && !string.IsNullOrWhiteSpace(Configuration["HttpsRedirectionPort"]))
            {
                services.AddHttpsRedirection(options =>
                {
                    options.HttpsPort = int.Parse(Configuration["HttpsRedirectionPort"]);
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KnightFrank.MemfusWongData.Api v1"));
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseApplicationIdentity();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotifyHub>("/notify");
            });

            //app.InitializeIdentityDatabase();
        }
    }
}
