using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("Property")]
    public partial class Property : ModelEntityBase
    {

        [Key]
        [Column("PropertyID")]
        public Guid PropertyID { get; set; }

        [Required]
        [StringLength(100)]
        public string Region { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string District { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string PropertyType { get; set; } = string.Empty;

        [Required]
        public decimal SalePrice { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string GrossArea { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string SaleableArea { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string YearBuilt { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string RefNo { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public decimal Area { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ListedDate { get; set; }

        [StringLength(200)]
        public string ImageUrl { get; set; } = string.Empty;

        //[InverseProperty(nameof(Location.Building))]
        //public virtual ICollection<Location> Locations { get; set; }
    }
}
