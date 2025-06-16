using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("Street")]
    public partial class Street : ModelEntityBase
    {
        public Street()
        {
            LocationStreet1s = new HashSet<Location>();
            LocationStreet2s = new HashSet<Location>();
        }

        [Key]
        [Column("StreetID")]
        public Guid StreetId { get; set; }
        [Required]
        [StringLength(35)]
        public string StreetName { get; set; }
        [Required]
        [StringLength(200)]
        public string StreetNameChin { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [StringLength(50)]
        public override string CreatedBy { get; set; }
        [Required]
        [StringLength(50)]
        public override string ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty(nameof(Location.Street1))]
        public virtual ICollection<Location> LocationStreet1s { get; set; }
        [InverseProperty(nameof(Location.Street2))]
        public virtual ICollection<Location> LocationStreet2s { get; set; }
    }
}
