using KnightFrank.BAL.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightFrank.BAL.Dtos.MemfusWongData
{
    public partial class DistrictDropdownDto
    {
        [Key]
        [KeywordSearch(false)]
        public string DistrictID { get; set; }

        [KeywordSearch("districtname")]
        public string DistrictName { get; set; }
        [KeywordSearch("districtnamechin")]
        public string DistrictNameChin { get; set; }
    }
}
