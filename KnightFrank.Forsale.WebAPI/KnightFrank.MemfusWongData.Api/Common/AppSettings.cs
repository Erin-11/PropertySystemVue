using System.Collections.Generic;

namespace KnightFrank.MemfusWongData.Api.Common
{
    internal class AppSettings
    {
        public class LogonAuthentication
        {
            public string Domain { get; set; }
            public LogonAuthenticationGenCode GenCode { get; set; }
            public int? AccessLifetime { get; set; }
        }

        internal class LogonAuthenticationGenCode
        {
            public int? MinimumRange { get; set; }
            public int? MaximumRange { get; set; }
        }

        public class Format
        {
            public string DateFormat { get; set; }
            public string DatetimeFormat { get; set; }
            public string DatetimeShortFormat { get; set; }
            public string NumberFormatWith3Decimal { get; set; }
            public string NumberFormatWith2Decimal { get; set; }
            public string NumberFormatWith1Decimal { get; set; }
            public string NumberFormatWithNoDecimal { get; set; }
            public string CurrencyFormatWith2Decimal { get; set; }
        }

        //public class MailSetting
        //{
        //    public string Host { get; set; }
        //    public int? Port { get; set; }
        //    public string Username { get; set; }
        //    public string Password { get; set; }
        //    public bool? EnableSSL { get; set; }
        //    public string SenderAddress { get; set; }
        //    public string SenderDisplayName { get; set; }
        //    public bool? EnableDBLog { get; set; }
        //    public int? ApplicationId { get; set; }
        //    public string Identifier { get; set; }

        //    public LogonEmail LogonEmail { get; set; }
        //}

        internal class LogonEmail
        {
            public string Subject { get; set; }
        }

        public class Logging
        {
            public LogLevel LogLevel { get; set; }
        }

        internal class LogLevel
        {
            public string Default { get; set; }
            public string Microsoft { get; set; }
            public string MicrosoftHostingLifetime { get; set; }
        }

        public class IdentityServerSetting
        {
            public IEnumerable<ApiScope> ApiScopes { get; set; }
            public IEnumerable<ApiResource> ApiResources { get; set; }
            public IEnumerable<Client> Clients { get; set; }
        }

        internal class ApiScope
        {
            public string Name { get; set; }
            public IEnumerable<string> UserClaims { get; set; }
        }

        internal class ApiResource
        {
            public string Name { get; set; }
            public string FriendlyName { get; set; }
            public IEnumerable<string> Scopes { get; set; }
        }

        internal class Client
        {
            public string ClientId { get; set; }
            public string ClientName { get; set; }
            public string ClientSecret { get; set; }
            public string AllowedGrantTypes { get; set; }
            public string AllowAccessTokensViaBrowser { get; set; }
            public string RequireClientSecret { get; set; }
            public IEnumerable<string> AllowedScopes { get; set; }
            public string AllowOfflineAccess { get; set; }
            public string RefreshTokenExpiration { get; set; }
            public string RefreshTokenUsage { get; set; }
            public string AccessTokenLifetime { get; set; }
        }
    }
}
