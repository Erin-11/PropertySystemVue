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
    public partial class BlockDropdownDto
    {
        [Key]
        [KeywordSearch(false)]
        public int? BlockID { get; set; }
        public string BlockIDString
        {
            get
            {
                return BlockID?.ToString() ?? string.Empty;
            }
        }
        [KeywordSearch("blockname")]
        public string BlockName { get; set; }
    }
}
