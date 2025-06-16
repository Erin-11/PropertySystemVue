using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("Phase")]
    public partial class Phase : ModelEntityBase
    {
        public Phase()
        {
            Locations = new HashSet<Location>();
        }

        [Key]
        [Column("PhaseID")]
        public int PhaseId { get; set; }
        [Required]
        [StringLength(50)]
        public string PhaseName { get; set; }
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

        [InverseProperty(nameof(Location.Phase))]
        public virtual ICollection<Location> Locations { get; set; }
    }
}
