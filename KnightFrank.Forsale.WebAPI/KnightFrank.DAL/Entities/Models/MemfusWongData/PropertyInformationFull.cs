using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Keyless]
    public partial class PropertyInformationFull : ModelEntityBase
    {
        [Required]
        [StringLength(200)]
        public string ZoneName { get; set; }
        [Required]
        [StringLength(200)]
        public string ZoneNameChin { get; set; }
        [Required]
        [StringLength(200)]
        public string DistrictName { get; set; }
        [StringLength(200)]
        public string DistrictNameChin { get; set; }
        [Required]
        [StringLength(35)]
        public string EstateName { get; set; }
        [Required]
        [StringLength(200)]
        public string EstateNameChin { get; set; }
        [Required]
        [StringLength(35)]
        public string BuildingName { get; set; }
        [Required]
        [StringLength(200)]
        public string BuildingNameChin { get; set; }
        [Required]
        [StringLength(35)]
        public string StreetName1 { get; set; }
        [Required]
        [StringLength(200)]
        public string StreetNameChin1 { get; set; }
        [StringLength(35)]
        public string StreetName2 { get; set; }
        [StringLength(200)]
        public string StreetNameChin2 { get; set; }
        [StringLength(10)]
        public string StreetNumberFrom1 { get; set; }
        [StringLength(10)]
        public string StreetNumberTo1 { get; set; }
        [StringLength(10)]
        public string StreetNumberFrom2 { get; set; }
        [StringLength(10)]
        public string StreetNumberTo2 { get; set; }
        [StringLength(25)]
        public string FloorName { get; set; }
        [Required]
        [StringLength(25)]
        public string UnitName { get; set; }
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
        [StringLength(200)]
        public string Dir { get; set; }
        [Column("PAT")]
        [StringLength(200)]
        public string Pat { get; set; }
        [Column("GUID")]
        public Guid Guid { get; set; }
        [Required]
        [StringLength(50)]
        public string PhaseName { get; set; }
        [Required]
        [StringLength(50)]
        public string BlockName { get; set; }
        [Column("LocationID")]
        public Guid LocationId { get; set; }
    }
}
