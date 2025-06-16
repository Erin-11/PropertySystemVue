using KnightFrank.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace KnightFrank.DAL.Entities.Models.MemfusWongData
{
    [Table("PPA")]
    public partial class Ppa : ModelEntityBase
    {
        [Key]
        [Column("GUID")]
        public Guid Guid { get; set; }
        [Column("source_name")]
        [StringLength(100)]
        public string SourceName { get; set; }
        [Required]
        [Column("MEM_NO")]
        [StringLength(20)]
        public string MemNo { get; set; }
        [Column("OP_DATE_YEAR")]
        public int? OpDateYear { get; set; }
        [Column("OP_DATE_MONTH")]
        public int? OpDateMonth { get; set; }
        [Column("DISTRICT_CODE")]
        [StringLength(50)]
        public string DistrictCode { get; set; }
        [Column("EST_NAME")]
        [StringLength(25)]
        public string EstName { get; set; }
        [Column("BLD_NAME")]
        [StringLength(35)]
        public string BldName { get; set; }
        [Column("STP")]
        [StringLength(5)]
        public string Stp { get; set; }
        [Column("BLOCK")]
        [StringLength(50)]
        public string Block { get; set; }
        [Column("LOT_NO1")]
        [StringLength(30)]
        public string LotNo1 { get; set; }
        [Column("LOT_NO2")]
        [StringLength(250)]
        public string LotNo2 { get; set; }
        [Column("ST_NAME1")]
        [StringLength(35)]
        public string StName1 { get; set; }
        [Column("ST_NO_FRM1")]
        [StringLength(10)]
        public string StNoFrm1 { get; set; }
        [Column("ST_NO_TO1")]
        [StringLength(10)]
        public string StNoTo1 { get; set; }
        [Column("ST_NAME2")]
        [StringLength(35)]
        public string StName2 { get; set; }
        [Column("ST_NO_FRM2")]
        [StringLength(10)]
        public string StNoFrm2 { get; set; }
        [Column("ST_NO_TO2")]
        [StringLength(10)]
        public string StNoTo2 { get; set; }
        [Column("FLOOR")]
        [StringLength(25)]
        public string Floor { get; set; }
        [Column("UNIT")]
        [StringLength(25)]
        public string Unit { get; set; }
        [Column("USAGE")]
        [StringLength(9)]
        public string Usage { get; set; }
        [Column("GFA")]
        public int? Gfa { get; set; }
        [Column("NFA")]
        public int? Nfa { get; set; }
        [Column("PROP_REFNO")]
        [StringLength(20)]
        public string PropRefno { get; set; }
        [Column("REM1")]
        [StringLength(55)]
        public string Rem1 { get; set; }
        [Column("REM2")]
        [StringLength(55)]
        public string Rem2 { get; set; }
        [Column("REM3")]
        [StringLength(55)]
        public string Rem3 { get; set; }
        [Column("REM4")]
        [StringLength(55)]
        public string Rem4 { get; set; }
        [Column("REM5")]
        [StringLength(55)]
        public string Rem5 { get; set; }
        public bool IsActive { get; set; }
        [StringLength(100)]
        public override string CreatedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public override DateTime? CreatedDate { get; set; }
        [StringLength(100)]
        public override string ModifiedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public override DateTime? ModifiedDate { get; set; }

        [ForeignKey(nameof(DistrictCode))]
        [InverseProperty(nameof(District.Ppas))]
        public virtual District DistrictCodeNavigation { get; set; }
        [ForeignKey(nameof(MemNo))]
        [InverseProperty(nameof(Mdbimpfl.Ppas))]
        public virtual Mdbimpfl MemNoNavigation { get; set; }
    }
}
