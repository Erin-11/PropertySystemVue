using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("Floor")]
    public partial class Floor : ModelEntityBase
    {
        public Floor()
        {
            PropertyInformations = new HashSet<PropertyInformation>();
        }

        [Key]
        [Column("FloorID")]
        public int FloorId { get; set; }
        [Required]
        [StringLength(25)]
        public string FloorName { get; set; }
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

        [InverseProperty(nameof(PropertyInformation.Floor))]
        public virtual ICollection<PropertyInformation> PropertyInformations { get; set; }
    }
}
