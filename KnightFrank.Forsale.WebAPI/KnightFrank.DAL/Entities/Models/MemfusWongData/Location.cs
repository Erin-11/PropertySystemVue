using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("Location")]
    public partial class Location : ModelEntityBase
    {
        public Location()
        {
            PropertyInformations = new HashSet<PropertyInformation>();
        }

        [Key]
        [Column("LocationID")]
        public Guid LocationId { get; set; }
        [Column("DistrictID")]
        [StringLength(10)]
        public string DistrictId { get; set; }
        [Column("PhaseID")]
        public int? PhaseId { get; set; }
        [Column("EstateID")]
        public Guid? EstateId { get; set; }
        [Column("BuildingID")]
        public Guid? BuildingId { get; set; }
        [Column("BlockID")]
        public int? BlockId { get; set; }
        [StringLength(50)]
        public string YearBuiltYear { get; set; }
        [StringLength(50)]
        public string YearBuiltMonth { get; set; }
        [Column("Street1ID")]
        public Guid? Street1Id { get; set; }
        [Column("Street2ID")]
        public Guid? Street2Id { get; set; }
        [StringLength(10)]
        public string StreetNumberFrom1 { get; set; }
        public int? StreetNumberFromNumeric1 { get; set; }
        [StringLength(10)]
        public string StreetNumberTo1 { get; set; }
        public int? StreetNumberToNumeric1 { get; set; }
        [StringLength(10)]
        public string StreetNumberFrom2 { get; set; }
        public int? StreetNumberFromNumeric2 { get; set; }
        [StringLength(10)]
        public string StreetNumberTo2 { get; set; }
        public int? StreetNumberToNumeric2 { get; set; }
        [StringLength(50)]
        public string GoogleMapLink { get; set; }
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

        [ForeignKey(nameof(BlockId))]
        [InverseProperty("Locations")]
        public virtual Block Block { get; set; }
        [ForeignKey(nameof(BuildingId))]
        [InverseProperty("Locations")]
        public virtual Building Building { get; set; }
        [ForeignKey(nameof(DistrictId))]
        [InverseProperty("Locations")]
        public virtual District District { get; set; }
        [ForeignKey(nameof(EstateId))]
        [InverseProperty("Locations")]
        public virtual Estate Estate { get; set; }
        [ForeignKey(nameof(Street1Id))]
        [InverseProperty(nameof(Street.LocationStreet1s))]
        public virtual Street Street1 { get; set; }
        [ForeignKey(nameof(Street2Id))]
        [InverseProperty(nameof(Street.LocationStreet2s))]
        public virtual Street Street2 { get; set; }
        [ForeignKey(nameof(PhaseId))]
        [InverseProperty("Locations")]
        public virtual Phase Phase { get; set; }
        [InverseProperty(nameof(PropertyInformation.Location))]
        public virtual ICollection<PropertyInformation> PropertyInformations { get; set; }
    }
}
