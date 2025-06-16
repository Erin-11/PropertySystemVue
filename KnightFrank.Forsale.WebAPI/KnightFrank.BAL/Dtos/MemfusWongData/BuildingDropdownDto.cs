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
    public partial class BuildingDropdownDto
    {
        [Key]
        [KeywordSearch(false)]
        public Guid BuildingID { get; set; }
        [KeywordSearch("block")]
        public string Block { get; set; }
        [KeywordSearch("buildingname")]
        public string BuildingName { get; set; }
        [KeywordSearch("buildingblockname")]
        public string BuildingBlockName
        {
            get
            {
                var resultString = string.Empty;

                if (!string.IsNullOrWhiteSpace(BuildingName))
                {
                    resultString += BuildingName.Trim();
                }

                if (!string.IsNullOrWhiteSpace(Block) && Block != Common.Constant.NotApplicable)
                {
                    resultString += " (Block " + Block.Trim() + ")";
                }

                return resultString ?? string.Empty;
            }
        }
        [KeywordSearch("buildingnamechin")]
        public string BuildingNameChin { get; set; }
        [KeywordSearch("buildingblocknamechin")]
        public string BuildingBlockNameChin
        {
            get
            {
                var resultString = string.Empty;

                if (!string.IsNullOrWhiteSpace(BuildingNameChin))
                {
                    resultString += BuildingNameChin.Trim();
                }

                if (!string.IsNullOrWhiteSpace(Block) && Block != Common.Constant.NotApplicable)
                {
                    resultString += " (" + Block.Trim() + "座" + ")";
                }

                return resultString ?? string.Empty;
            }
        }
    }
}
