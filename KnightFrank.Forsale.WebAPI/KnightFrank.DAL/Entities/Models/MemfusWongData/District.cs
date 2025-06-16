using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("District")]
    public partial class District : ModelEntityBase
    {
        public District()
        {
            Locations = new HashSet<Location>();
            Ppas = new HashSet<Ppa>();
        }

        [Key]
        [Column("DistrictID")]
        [StringLength(10)]
        public string DistrictId { get; set; }
        [Column("ZoneID")]
        [StringLength(50)]
        public string ZoneId { get; set; }
        [Required]
        [StringLength(200)]
        public string DistrictName { get; set; }
        [StringLength(200)]
        public string DistrictNameChin { get; set; }
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

        [ForeignKey(nameof(ZoneId))]
        [InverseProperty("Districts")]
        public virtual Zone Zone { get; set; }
        [InverseProperty(nameof(Location.District))]
        public virtual ICollection<Location> Locations { get; set; }
        [InverseProperty(nameof(Ppa.DistrictCodeNavigation))]
        public virtual ICollection<Ppa> Ppas { get; set; }
    }
}
