using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using KnightFrank.AuthServer.Authentication;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KnightFrank.MemfusWongData.Api.Helpers
{
    internal class Utility
    {
        internal static Stream GetResourceStream(string fileName)
        {
            var assembly = typeof(Startup).Assembly;
            var resourcePaths = assembly.GetManifestResourceNames().Where(e => e.Contains(fileName));
            if (resourcePaths.Count() == 1)
            {
                return assembly.GetManifestResourceStream(resourcePaths.First());
            }
            return null;
        }

        internal static string GetResourceString(string fileName)
        {
            var assembly = typeof(Startup).Assembly;
            var resourcePaths = assembly.GetManifestResourceNames().Where(e => e.Contains(fileName));
            if (resourcePaths.Count() == 1)
            {
                var resourceStream = assembly.GetManifestResourceStream(resourcePaths.First());
                using var sr = new StreamReader(resourceStream);
                var str = sr.ReadToEnd();
                sr.Close();
                return str;
            }
            return null;
        }

        internal static ICollection<string> GetIdentityGrantTypes(string type)
        {
            if (string.IsNullOrEmpty(type))
                return GrantTypes.ClientCredentials;

            return type.ToLower() switch
            {
                "implicit" => GrantTypes.Implicit,
                "implicitandclientcredentials" => GrantTypes.ImplicitAndClientCredentials,
                "code" => GrantTypes.Code,
                "codeandclientcredentials" => GrantTypes.CodeAndClientCredentials,
                "hybrid" => GrantTypes.Hybrid,
                "hybridandclientcredentials" => GrantTypes.HybridAndClientCredentials,
                "resourceownerpassword" => GrantTypes.ResourceOwnerPassword,
                "resourceownerpasswordandclientcredentials" => GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                _ => GrantTypes.ClientCredentials,
            };
        }

        internal static bool GetAllowAccessTokensViaBrowser(string tokensViaBrowser)
        {
            if (string.IsNullOrEmpty(tokensViaBrowser))
                return false;

            return tokensViaBrowser.ToLower() switch
            {
                "true" or "1" or "yes" => true,
                _ => false,
            };
        }

        internal static bool GetRequireClientSecret(string clientSecret)
        {
            if (string.IsNullOrEmpty(clientSecret))
                return true;

            return clientSecret.ToLower() switch
            {
                "false" or "0" or "no" => false,
                _ => true,
            };
        }

        internal static ICollection<string> GetAllowedScopes(IEnumerable<string> scopes)
        {
            var _default = new List<string>()
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.Phone,
                IdentityServerConstants.StandardScopes.Email,
                ScopeConstants.Roles
            };

            if (scopes == null || !scopes.Any())
                return _default;

            return _default.Concat(scopes).ToList();
        }

        internal static bool GetAllowOfflineAccess(string offlineAccess)
        {
            if (string.IsNullOrEmpty(offlineAccess))
                return false;

            return offlineAccess.ToLower() switch
            {
                "true" or "1" or "yes" => true,
                _ => false,
            };
        }

        internal static TokenExpiration GetRefreshTokenExpiration(string tokenExpiration)
        {
            if (string.IsNullOrEmpty(tokenExpiration))
                return TokenExpiration.Sliding;

            return tokenExpiration.ToLower() switch
            {
                "absolute" => TokenExpiration.Absolute,
                _ => TokenExpiration.Sliding,
            };
        }

        internal static TokenUsage GetRefreshTokenUsage(string tokenUsage)
        {
            if (string.IsNullOrEmpty(tokenUsage))
                return TokenUsage.ReUse;

            return tokenUsage.ToLower() switch
            {
                "onetimeonly" => TokenUsage.OneTimeOnly,
                _ => TokenUsage.ReUse,
            };
        }

        internal static int GetAccessTokenLifetime(string lifetime)
        {
            if (int.TryParse(lifetime, out int value))
            {
                return value;
            }

            return 3600;
        }

        internal static ICollection<string> GetApiScopeUserClaims(IEnumerable<string> userClaims)
        {
            var _default = new List<string>()
            {
                JwtClaimTypes.Name,
                JwtClaimTypes.Email,
                JwtClaimTypes.PhoneNumber,
                JwtClaimTypes.Role,
                ClaimConstants.Permission,
            };

            if (userClaims == null || !userClaims.Any())
                return _default;

            return _default.Concat(userClaims).ToList();
        }

        //public const string ParameterId = "id";
        //public const string ParameterPropertyId = "propertyId";
        //public const string ParameterBatchId = "batchId";
        //public const string ParameterShowWaitingApprovalOnly = "showWaitingApprovalOnly";
        //public const string ParameterPropertyUnitId = "propertyUnitId";
        //public const string ParameterFilterBlock = "block";
        //public const string ParameterFilterFloor = "floor";
        //public const string ParameterPriceListRevisionId = "priceListRevisionId";
        //public const string ParameterShowAttendOnly = "showAttendOnly";
        //public const string ParameterFilterType = "filterType";
        //public const string ParameterFilterText = "filterText";
        //public const string ParameterExcludeIds = "excludeIds";

        //public static string GetUserId(ClaimsPrincipal user)
        //{
        //    return user.FindFirst(JwtClaimTypes.Subject)?.Value?.Trim();
        //}

        //public static IEnumerable<string> GetRoles(ClaimsPrincipal identity)
        //{
        //    return identity.Claims
        //        .Where(c => c.Type == JwtClaimTypes.Role)
        //        .Select(c => c.Value);
        //}
    }

    public static class DataTableModelBindingParser
    {
        //public static IDictionary<string, object> Parser(ModelBindingContext modelBindingContext)
        //{
        //    //var p = Convert.ToString(modelBindingContext.ValueProvider.GetValue("id").FirstValue);
        //    //var p1 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("propertyId").FirstValue);
        //    //var p2 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("batchId").FirstValue);
        //    //var p3 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("showWaitingApprovalOnly").FirstValue);
        //    //var p4 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("propertyUnitId").FirstValue);
        //    //var p5 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("block").FirstValue);
        //    //var p6 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("floor").FirstValue);
        //    //var p7 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("priceListRevisionId").FirstValue);
        //    //var p8 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("showAttendOnly").FirstValue);
        //    //var p9 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("filterType").FirstValue);
        //    //var p10 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("filterText").FirstValue);
        //    //var p11 = Convert.ToString(modelBindingContext.ValueProvider.GetValue("excludeIds").FirstValue);

        //    //return new Dictionary<string, object>() {
        //    //    { "id", p },
        //    //    { "propertyId", p1 },
        //    //    { "batchId", p2 },
        //    //    { "showWaitingApprovalOnly", p3 },
        //    //    { "propertyUnitId", p4 },
        //    //    { "block", p5 },
        //    //    { "floor", p6 },
        //    //    { "priceListRevisionId", p7 },
        //    //    { "showAttendOnly", p8 },
        //    //    { "filterType", p9 },
        //    //    { "filterText", p10 },
        //    //    { "excludeIds", p11 }
        //    //};
        //}
    }
}
