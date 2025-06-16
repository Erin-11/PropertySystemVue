using Microsoft.AspNetCore.Identity;

namespace KnightFrank.AuthServer.DAL.Entities.Models
{
    public class ApplicationUserLogin : IdentityUserLogin<int>
    {
        public virtual ApplicationUser User { get; set; }
    }
}
