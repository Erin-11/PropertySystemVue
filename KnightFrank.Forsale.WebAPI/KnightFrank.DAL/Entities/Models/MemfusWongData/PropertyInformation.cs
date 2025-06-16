using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("PropertyInformation")]
    public partial class PropertyInformation : ModelEntityBase
    {
        [Key]
        [Column("GUID")]
        public Guid Guid { get; set; }
        [Column("LocationID")]
        public Guid LocationId { get; set; }
        [Column("FloorID")]
        public int? FloorId { get; set; }
        [Column("UnitID")]
        public int? UnitId { get; set; }
        [StringLength(9)]
        public string Usage { get; set; }
        [Column("GFA")]
        public int? Gfa { get; set; }
        [Column("NFA")]
        public int? Nfa { get; set; }
        public int? BayWindow { get; set; }
        public int? Balcony { get; set; }
        public int? UtilityPlatform { get; set; }
        [Column("ACRoom")]
        public int? Acroom { get; set; }
        [Column("ACPlatform")]
        public int? Acplatform { get; set; }
        public int? FlatRoof { get; set; }
        public int? Roof { get; set; }
        public int? Garden { get; set; }
        public int? Terrace { get; set; }
        public int? Yard { get; set; }
        public int? Cockloft { get; set; }
        [Column("DIR")]
        [StringLength(500)]
        public string Dir { get; set; }
        [Column("PAT")]
        [StringLength(500)]
        public string Pat { get; set; }
        public bool IsActive { get; set; }
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

        [ForeignKey(nameof(FloorId))]
        [InverseProperty("PropertyInformations")]
        public virtual Floor Floor { get; set; }
        [ForeignKey(nameof(LocationId))]
        [InverseProperty("PropertyInformations")]
        public virtual Location Location { get; set; }
        [ForeignKey(nameof(UnitId))]
        [InverseProperty("PropertyInformations")]
        public virtual Unit Unit { get; set; }
    }
}
