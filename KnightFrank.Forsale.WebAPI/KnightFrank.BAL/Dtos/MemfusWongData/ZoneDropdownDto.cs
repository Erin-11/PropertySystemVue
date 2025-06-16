using KnightFrank.BAL.Attributes;
using System.ComponentModel.DataAnnotations;

namespace KnightFrank.BAL.Dtos.MemfusWongData
{
    public partial class ZoneDropdownDto
    {
        [Key]
        [KeywordSearch(false)]
        public string ZoneID { get; set; }

        [KeywordSearch("zonename")]
        public string ZoneName { get; set; }


        [KeywordSearch("zonenamechin")]
        public string ZoneNameChin { get; set; }
    }
}
