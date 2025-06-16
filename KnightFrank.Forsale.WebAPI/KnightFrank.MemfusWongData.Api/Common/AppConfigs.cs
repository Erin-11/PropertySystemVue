using static KnightFrank.MemfusWongData.Api.Common.AppSettings;

namespace KnightFrank.MemfusWongData.Api.Common
{
    internal static class AppConfigs
    {
        public static LogonAuthentication EmailLogon { get; set; } = new LogonAuthentication();
        public static Format Format { get; set; } = new Format();
        public static Logging Logging { get; set; } = new Logging();
        public static IdentityServerSetting IdentityServerSetting { get; set; } = new IdentityServerSetting();
    }
}
