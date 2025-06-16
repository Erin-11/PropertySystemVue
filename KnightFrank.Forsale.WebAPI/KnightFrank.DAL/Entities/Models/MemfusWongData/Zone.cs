using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("Zone")]
    public partial class Zone : ModelEntityBase
    {
        public Zone()
        {
            Districts = new HashSet<District>();
        }

        [Key]
        [Column("ZoneID")]
        [StringLength(50)]
        public string ZoneId { get; set; }
        [Required]
        [StringLength(200)]
        public string ZoneName { get; set; }
        [Required]
        [StringLength(200)]
        public string ZoneNameChin { get; set; }
        [Required]
        [StringLength(50)]
        public override string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(50)]
        public override string ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        [InverseProperty(nameof(District.Zone))]
        public virtual ICollection<District> Districts { get; set; }
    }
}
