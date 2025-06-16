using Microsoft.AspNetCore.Identity;

namespace KnightFrank.AuthServer.DAL.Entities.Models
{
    public class ApplicationUserClaim : IdentityUserClaim<int>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
