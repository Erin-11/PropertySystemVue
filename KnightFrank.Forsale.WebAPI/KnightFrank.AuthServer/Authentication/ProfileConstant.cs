﻿namespace KnightFrank.AuthServer.Authentication
{
    public static class ClaimConstants
    {
        ///<summary>A claim that specifies the subject of an entity</summary>
        public const string Subject = "sub";

        ///<summary>A claim that specifies the permission of an entity</summary>
        public const string Permission = "permission";
    }

    public static class PropertyConstants
    {

        ///<summary>A property that specifies the full name of an entity</summary>
        public const string DisplayName = "displayname";

        ///<summary>A property that specifies the job title of an entity</summary>
        public const string Title = "title";

        ///<summary>A property that specifies the configuration/customizations of an entity</summary>
        public const string Configuration = "configuration";
    }

    public static class ScopeConstants
    {
        ///<summary>A scope that specifies the roles of an entity</summary>
        public const string Roles = "roles";
    }
}
