using Microsoft.AspNetCore.Identity;

namespace KnightFrank.AuthServer.DAL.Entities.Models
{
    public class ApplicationUserRole : IdentityUserRole<int>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
