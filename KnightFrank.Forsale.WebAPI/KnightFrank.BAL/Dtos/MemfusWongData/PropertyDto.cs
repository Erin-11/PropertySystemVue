using KnightFrank.BAL.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace KnightFrank.BAL.Dtos.MemfusWongData
{
    public partial class PropertyDto
    {
        [Key]
        [KeywordSearch("PropertyID")]
        public int PropertyID { get; set; }

        [KeywordSearch("region")]
        public string Region { get; set; } = string.Empty;

        [KeywordSearch("district")]
        public string District { get; set; } = string.Empty;

        [KeywordSearch("propertytype")]
        public string PropertyType { get; set; } = string.Empty;

        public decimal SalePrice { get; set; }

        public string Address { get; set; } = string.Empty;

        public string GrossArea { get; set; } = string.Empty;

        public string SaleableArea { get; set; } = string.Empty;

        public string YearBuilt { get; set; } = string.Empty;

        public string RefNo { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public decimal Area { get; set; }

        public DateTime ListedDate { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

    }
}
