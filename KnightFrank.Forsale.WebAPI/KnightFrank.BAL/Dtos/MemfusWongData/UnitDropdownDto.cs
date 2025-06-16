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
    public partial class UnitDropdownDto
    {
        [Key]
        [KeywordSearch(false)]
        public int? UnitID { get; set; }
        public string UnitIDString
        {
            get
            {
                return UnitID?.ToString() ?? string.Empty;
            }
        }

        [KeywordSearch("unitname")]
        public string UnitName { get; set; }
    }
}
