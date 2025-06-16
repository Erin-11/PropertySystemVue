using KnightFrank.DataAccessLayer.EF.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnightFrank.AuthServer.DAL.Entities.Models
{
    public class ApplicationUser : IdentityUser<int>, IObjectState
    {
        public ApplicationUser()
            : this(0) { }
        public ApplicationUser(int id)
        {
            Id = id;
            Claims = new List<ApplicationUserClaim>();
            Logins = new List<ApplicationUserLogin>();
            Tokens = new List<ApplicationUserToken>();
            UserRoles = new List<ApplicationUserRole>();
        }

        public string DisplayName { get; set; }

        public string Title { get; set; }

        public Guid? IdentityGroupID { get; set; }

        public string Configuration { get; set; }

        public bool IsActive { get; set; }

        //public bool IsDeletedRecord { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        //[DataType(DataType.DateTime)]
        //public DateTime? DeletedDateTime { get; set; }
        //public Guid? DeletedUserID { get; set; }

        //[Timestamp]
        //public byte[] RowVersion { get; set; }

        [NotMapped]
        public ObjectState ObjectState { get; set; }

        [NotMapped]
        public string UpdatedUserName { get; set; }

        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
