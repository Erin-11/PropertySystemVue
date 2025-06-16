using KnightFrank.BAL.Attributes;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightFrank.BAL.Dtos.MemfusWongData
{
    public partial class PhaseDropdownDto
    {
        [Key]
        [KeywordSearch(false)]
        public int? PhaseID { get; set; }
        public string PhaseIDString
        {
            get
            {
                return PhaseID?.ToString() ?? string.Empty;
            }
        }
        [KeywordSearch("phasename")]
        public string PhaseName { get; set; }
    }
}
