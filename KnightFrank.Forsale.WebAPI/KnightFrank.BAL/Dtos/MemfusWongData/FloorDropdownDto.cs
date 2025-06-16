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
    public partial class FloorDropdownDto
    {
        [Key]
        [KeywordSearch(false)]
        public int? FloorID { get; set; }
        public string FloorIDString
        {
            get
            {
                return FloorID?.ToString() ?? string.Empty;
            }
        }

        [KeywordSearch("floorname")]
        public string FloorName { get; set; }
    }
}
