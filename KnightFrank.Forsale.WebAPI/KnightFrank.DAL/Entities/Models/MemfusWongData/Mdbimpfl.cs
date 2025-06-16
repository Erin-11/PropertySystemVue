using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("MDBIMPFL")]
    public partial class Mdbimpfl : ModelEntityBase
    {
        public Mdbimpfl()
        {
            Ppas = new HashSet<Ppa>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Column("SOURCE_NAME")]
        [StringLength(100)]
        public string SourceName { get; set; }
        [Key]
        [Column("MEM_NO")]
        [StringLength(20)]
        public string MemNo { get; set; }
        [Column("INSTRUMENT_DATE", TypeName = "datetime")]
        public DateTime? InstrumentDate { get; set; }
        [Column("DELIVERY_DATE", TypeName = "datetime")]
        public DateTime? DeliveryDate { get; set; }
        [Column("NATURE")]
        [StringLength(10)]
        public string Nature { get; set; }
        [Column("CONSIDERTION", TypeName = "decimal(18, 3)")]
        public decimal? Considertion { get; set; }
        [Column("SOLICITORS")]
        [StringLength(200)]
        public string Solicitors { get; set; }
        [Column("TOTAL_GFA", TypeName = "decimal(18, 2)")]
        public decimal? TotalGfa { get; set; }
        [Column("TOTAL_NFA", TypeName = "decimal(18, 2)")]
        public decimal? TotalNfa { get; set; }
        [Column("GROSS_PSF", TypeName = "decimal(18, 2)")]
        public decimal? GrossPsf { get; set; }
        [Column("NET_PSF", TypeName = "decimal(18, 2)")]
        public decimal? NetPsf { get; set; }
        public bool IsActive { get; set; }
        [StringLength(100)]
        public override string CreatedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public override DateTime? CreatedDate { get; set; }
        [StringLength(100)]
        public override string ModifiedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public override DateTime? ModifiedDate { get; set; }

        [InverseProperty(nameof(Ppa.MemNoNavigation))]
        public virtual ICollection<Ppa> Ppas { get; set; }
    }
}
