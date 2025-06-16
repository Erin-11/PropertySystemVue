using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("Building")]
    public partial class Building : ModelEntityBase
    {
        public Building()
        {
            Locations = new HashSet<Location>();
        }

        [Key]
        [Column("BuildingID")]
        public Guid BuildingId { get; set; }
        [Required]
        [StringLength(35)]
        public string BuildingName { get; set; }
        [Required]
        [StringLength(200)]
        public string BuildingNameChin { get; set; }
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

        [InverseProperty(nameof(Location.Building))]
        public virtual ICollection<Location> Locations { get; set; }
    }
}
