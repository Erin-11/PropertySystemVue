using KnightFrank.BAL.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightFrank.BAL.Dtos.MemfusWongData
{
    public partial class StreetDropdownDto
    {
        [Key]
        [KeywordSearch(false)]
        public Guid StreetID { get; set; }

        [KeywordSearch("streetname")]
        public string StreetName { get; set; }
        [KeywordSearch("streetnamechin")]
        public string StreetNameChin { get; set; }
    }
}
