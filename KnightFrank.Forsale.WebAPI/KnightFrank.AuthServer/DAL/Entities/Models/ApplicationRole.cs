using KnightFrank.DataAccessLayer.EF.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnightFrank.AuthServer.DAL.Entities.Models
{
    public class ApplicationRole : IdentityRole<int>, IObjectState
    {
        public ApplicationRole()
            : this(0) { }
        public ApplicationRole(int id)
        {
            Id = id;
            UserRoles = new List<ApplicationUserRole>();
            RoleClaims = new List<ApplicationRoleClaim>();
        }

        public string Description { get; set; }

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

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }
}
