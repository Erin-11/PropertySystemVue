using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("Block")]
    public partial class Block : ModelEntityBase
    {
        public Block()
        {
            Locations = new HashSet<Location>();
        }

        [Key]
        [Column("BlockID")]
        public int BlockId { get; set; }
        [Required]
        [StringLength(50)]
        public string BlockName { get; set; }
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

        [InverseProperty(nameof(Location.Block))]
        public virtual ICollection<Location> Locations { get; set; }
    }
}
