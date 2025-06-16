using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using KnightFrank.AuthServer;
using KnightFrank.AuthServer.Configuration;
using KnightFrank.BAL.Configuration;
using KnightFrank.DAL.Configuration;
using KnightFrank.MemfusWongData.Api.Common;
using KnightFrank.MemfusWongData.Api.Filters;
using KnightFrank.MemfusWongData.Api.Helpers.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = builder.Configuration;
var environment = builder.Environment;

var assembly = Assembly.GetExecutingAssembly().GetName().Name;
var memfusWongDataDBConnectionString = configuration.GetConnectionString("MemfusWongDataDB");
var applicationUrl = configuration["ApplicationUrl"]?.TrimEnd('/');
var signingCredential = configuration["SigningCredential"];
var validationKey = configuration["ValidationKey"];

configuration.GetSection("Format").Bind(AppConfigs.Format);
configuration.GetSection("IdentityServerSetting").Bind(AppConfigs.IdentityServerSetting);

// Razor Pages root directory
builder.Services.Configure<Microsoft.AspNetCore.Mvc.RazorPages.RazorPagesOptions>(opt => opt.RootDirectory = "/WebApi/Pages");

// IdentityServer clients/resources/scopes
var clients = AppConfigs.IdentityServerSetting.Clients?.Select(e => new IdentityServer4.Models.Client
{
    ClientId = e.ClientId,
    ClientName = e.ClientName,
    ClientSecrets = { new IdentityServer4.Models.Secret(e.ClientSecret.Sha256()) },
    AllowedGrantTypes = KnightFrank.MemfusWongData.Api.Helpers.Utility.GetIdentityGrantTypes(e.AllowedGrantTypes),
    AllowAccessTokensViaBrowser = KnightFrank.MemfusWongData.Api.Helpers.Utility.GetAllowAccessTokensViaBrowser(e.AllowAccessTokensViaBrowser),
    RequireClientSecret = KnightFrank.MemfusWongData.Api.Helpers.Utility.GetRequireClientSecret(e.RequireClientSecret),
    AllowedScopes = KnightFrank.MemfusWongData.Api.Helpers.Utility.GetAllowedScopes(e.AllowedScopes),
    AllowOfflineAccess = KnightFrank.MemfusWongData.Api.Helpers.Utility.GetAllowOfflineAccess(e.AllowOfflineAccess),
    RefreshTokenExpiration = KnightFrank.MemfusWongData.Api.Helpers.Utility.GetRefreshTokenExpiration(e.RefreshTokenExpiration),
    AccessTokenLifetime = KnightFrank.MemfusWongData.Api.Helpers.Utility.GetAccessTokenLifetime(e.AccessTokenLifetime)
});
var apiResources = AppConfigs.IdentityServerSetting.ApiResources?.Select(e => new IdentityServer4.Models.ApiResource(e.Name, e.FriendlyName)
{
    Scopes = e.Scopes.ToList()
});
var apiScopes = AppConfigs.IdentityServerSetting.ApiScopes?.Select(e => new IdentityServer4.Models.ApiScope(e.Name)
{
    UserClaims = KnightFrank.MemfusWongData.Api.Helpers.Utility.GetApiScopeUserClaims(e.UserClaims)
});

// Add application services
builder.Services.AddApplicationDbContext<KnightFrank.DAL.MemfusWongDataEntities>(memfusWongDataDBConnectionString, assembly);
builder.Services.AddApplicationDataRepository();
builder.Services.AddApplicationService();
builder.Services.AddApplicationIdentity(environment.IsDevelopment(), signingCredential, validationKey, clients: clients, apiResources: apiResources, apiScopes: apiScopes);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => policy
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// SignalR
builder.Services.AddSignalR();

// Authentication
builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = applicationUrl;
        options.SupportedTokens = IdentityServer4.AccessTokenValidation.SupportedTokens.Jwt;
        options.RequireHttpsMetadata = false; // Set to true in production
        options.ApiName = IdentityServerConfig.ApiName;
        options.LegacyAudienceValidation = true;
    });

// Authorization
builder.Services.AddAuthorization();

// Controllers and Filters
builder.Services.AddControllers(options =>
{
    options.EnableEndpointRouting = false;
    options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder(IdentityServerAuthenticationDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build()));
    options.Filters.Add(new GlobalActionFilter());
    options.Filters.Add(new GlobalExceptoinFilter());
}).AddNewtonsoftJson();

// Routing
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Swagger
builder.Services.AddSwaggerGen(c =>
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

// HTTPS Redirection
if (!environment.IsDevelopment() && !string.IsNullOrWhiteSpace(configuration["HttpsRedirectionPort"]))
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.HttpsPort = int.Parse(configuration["HttpsRedirectionPort"]);
    });
}

var app = builder.Build();

// Pipeline
if (app.Environment.IsDevelopment())
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
    _ = endpoints.MapControllers();
    _ = endpoints.MapHub<NotifyHub>("/notify");
});

app.Run();
