using KnightFrank.BAL.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightFrank.BAL.Dtos.MemfusWongData
{
    public partial class EstateDropdownDto
    {
        [Key]
        [KeywordSearch(false)]
        public Guid EstateID { get; set; }
        [KeywordSearch("estatename")]
        public string EstateName { get; set; }
        [KeywordSearch("estatenamechin")]
        public string EstateNameChin { get; set; }
    }
}
