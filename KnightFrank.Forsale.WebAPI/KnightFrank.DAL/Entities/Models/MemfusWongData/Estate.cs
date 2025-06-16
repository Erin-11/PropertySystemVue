using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("Estate")]
    public partial class Estate : ModelEntityBase
    {
        public Estate()
        {
            Locations = new HashSet<Location>();
        }

        [Key]
        [Column("EstateID")]
        public Guid EstateId { get; set; }
        [Required]
        [StringLength(35)]
        public string EstateName { get; set; }
        [Required]
        [StringLength(200)]
        public string EstateNameChin { get; set; }
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

        [InverseProperty(nameof(Location.Estate))]
        public virtual ICollection<Location> Locations { get; set; }
    }
}
