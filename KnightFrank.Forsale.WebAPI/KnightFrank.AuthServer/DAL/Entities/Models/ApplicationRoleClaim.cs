using Microsoft.AspNetCore.Identity;

namespace KnightFrank.AuthServer.DAL.Entities.Models
{
    public class ApplicationRoleClaim : IdentityRoleClaim<int>
    {
        public ApplicationRoleClaim()
        {
        }
        public ApplicationRoleClaim(string type, string value)
        {
            ClaimType = type;
            ClaimValue = value;
        }

        public virtual ApplicationRole Role { get; set; }
    }
}
