using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using KnightFrank.AuthServer.Authentication;
using KnightFrank.AuthServer.Authorization.Common;
using System.Collections.Generic;

namespace KnightFrank.AuthServer
{
    public class IdentityServerConfig
    {
        public const string ApiName = "kf_iprop_api";
        public const string ApiFriendlyName = "Knight Frank API";
        public const string ApiScopeName = "kf_iprop_scopes";
        public const string ClientID = "kf_iprop_client";
        public const string ClientSecret = "kf_iprop_client_secret";
        public const string SwaggerClientID = "swaggerui";

        // Identity resources (used by UserInfo endpoint).
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone(),
                new IdentityResources.Email(),
                new IdentityResource(ScopeConstants.Roles, new List<string> { JwtClaimTypes.Role })
            };
        }

        // Api scopes.
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(ApiScopeName) {
                    UserClaims = {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.PhoneNumber,
                        JwtClaimTypes.Role,
                        ClaimConstants.Permission,
                        ApplicationPermissions.viewAgents
                    }
                }
            };
        }

        // Api resources.
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ApiName) {
                    Scopes = new List<string>()
                    {
                        ApiScopeName
                    },
                    //UserClaims = {
                    //    JwtClaimTypes.Name,
                    //    JwtClaimTypes.Email,
                    //    JwtClaimTypes.PhoneNumber,
                    //    JwtClaimTypes.Role,
                    //    ClaimConstants.Permission
                    //}
                }
            };
        }

        // Clients want to access resources.
        public static IEnumerable<Client> GetClients()
        {
            // Clients credentials.
            return new List<Client>
            {
                // http://docs.identityserver.io/en/release/reference/client.html.
                new Client
                {
                    ClientId = ClientID,
                    ClientName = "Knight Frank Client",
                    ClientSecrets = {
                        new Secret(ClientSecret.Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials, // Resource Owner Password Credential grant.
                    AllowAccessTokensViaBrowser = true,
                    RequireClientSecret = true, // This client does not need a secret to request tokens from the token endpoint.
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId, // For UserInfo endpoint.
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        ScopeConstants.Roles,
                        ApiScopeName,
                    },
                    AllowOfflineAccess = true, // For refresh token.
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AccessTokenLifetime = 86400, // Lifetime of access token in seconds.
                    //AbsoluteRefreshTokenLifetime = 7200,
                    //SlidingRefreshTokenLifetime = 900,
                },

                //new Client
                //{
                //    ClientId = SwaggerClientID,
                //    ClientName = "Swagger UI",
                //    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                //    AllowAccessTokensViaBrowser = true,
                //    RequireClientSecret = false,
                //    AllowedScopes = {
                //        ApiName
                //    }
                //}
            };
        }
    }
}
