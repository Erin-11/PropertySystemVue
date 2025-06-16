using KnightFrank.BAL.Attributes;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KnightFrank.BAL.Dtos.MemfusWongData
{
    public partial class PropertyInformationDto : BaseDto
    {
        [IgnoreDataMember]
        public Location Location { get; set; }
        [IgnoreDataMember]
        public Floor Floor { get; set; }
        [IgnoreDataMember]
        public Unit Unit { get; set; }


        [Key]
        [KeywordSearch(false)]
        public Guid Guid { get; set; }
        [KeywordSearch("zonename")]
        public string ZoneName
        {
            get
            {
                return Location?.District?.Zone?.ZoneName ?? string.Empty;
            }
        }
        [KeywordSearch("zonenamechin")]
        public string ZoneNameChin
        {
            get
            {
                return Location?.District?.Zone?.ZoneNameChin ?? string.Empty;
            }
        }
        public string DistrictID
        {
            get
            {
                return Location?.District?.DistrictId ?? string.Empty;
            }
        }
        [KeywordSearch("districtname")]
        public string DistrictName
        {
            get
            {
                return Location?.District?.DistrictName ?? string.Empty;
            }
        }
        [KeywordSearch("districtnamechin")]
        public string DistrictNameChin
        {
            get
            {
                return Location?.District?.DistrictNameChin ?? string.Empty;
            }
        }
        [KeywordSearch("streetname1")]
        public string StreetName1
        {
            get
            {
                return Location?.Street1?.StreetName ?? string.Empty;
            }
        }
        [KeywordSearch("streetnamechin1")]
        public string StreetNameChin1
        {
            get
            {
                return Location?.Street1?.StreetNameChin ?? string.Empty;
            }
        }
        [KeywordSearch("streetnofrom1")]
        public string StreetNoFrom1
        {
            get
            {
                return Location?.StreetNumberFrom1 ?? string.Empty;
            }
        }
        [KeywordSearch("streetnoto1")]
        public string StreetNoTo1
        {
            get
            {
                return Location?.StreetNumberTo1 ?? string.Empty;
            }
        }
        [KeywordSearch("streetname2")]
        public string StreetName2
        {
            get
            {
                return Location?.Street2?.StreetName ?? string.Empty;
            }
        }
        [KeywordSearch("streetnamechin2")]
        public string StreetNameChin2
        {
            get
            {
                return Location?.Street2?.StreetNameChin ?? string.Empty;
            }
        }
        [KeywordSearch("streetnofrom2")]
        public string StreetNoFrom2
        {
            get
            {
                return Location?.StreetNumberFrom2 ?? string.Empty;
            }
        }
        [KeywordSearch("streetnoto2")]
        public string StreetNoTo2
        {
            get
            {
                return Location?.StreetNumberTo2 ?? string.Empty;
            }
        }
        [KeywordSearch("estate")]
        public string Estate
        {
            get
            {
                return Location?.Estate?.EstateName ?? string.Empty;
            }
        }
        [KeywordSearch("estatechin")]
        public string EstateChin
        {
            get
            {
                return Location?.Estate?.EstateNameChin ?? string.Empty;
            }
        }
        [KeywordSearch("building")]
        public string Building
        {
            get
            {
                return Location?.Building?.BuildingName ?? string.Empty;
            }
        }
        [KeywordSearch("buildingchin")]
        public string BuildingChin
        {
            get
            {
                return Location?.Building?.BuildingNameChin ?? string.Empty;
            }
        }
        public string Phase
        {
            get
            {
                return Location?.Phase?.PhaseName ?? string.Empty;
            }
        }
        public string Block
        {
            get
            {
                return Location?.Block?.BlockName ?? string.Empty;
            }
        }
        public string YearBuiltYear
        {
            get
            {
                return Location?.YearBuiltYear ?? string.Empty;
            }
        }
        public string YearBuiltMonth
        {
            get
            {
                return Location?.YearBuiltMonth ?? string.Empty;
            }
        }
        public string YearBuilt
        {
            get
            {
                return YearBuiltYear + "-" + YearBuiltMonth;
            }
        }
        [KeywordSearch("floor")]
        public string FloorText
        {
            get
            {
                return Floor.FloorName ?? string.Empty;
            }
        }
        [KeywordSearch("unit")]
        public string UnitText
        {
            get
            {
                return Unit.UnitName ?? string.Empty;
            }
        }
        public string FullAddressLine1
        {
            get
            {
                return (string.IsNullOrEmpty(UnitText) ? string.Empty : ("Unit " + UnitText.Trim() + ", ")) + (string.IsNullOrEmpty(FloorText) ? string.Empty : ("Floor " + FloorText.Trim() + ", "));
            }
        }
        public string FullAddressLine2
        {
            get
            {
                return (string.IsNullOrEmpty(Building) ? string.Empty : Building.Trim()) + (string.IsNullOrEmpty(Block) ? ", " : " (Block " + Block.Trim() + ")" + ", ");
            }
        }
        public string FullAddressLine3
        {
            get
            {
                return (string.IsNullOrEmpty(StreetNoFrom1) ? string.Empty : StreetNoFrom1.Trim()) + (string.IsNullOrEmpty(StreetNoTo1) ? string.Empty : "-" + StreetNoTo1.Trim()) + (string.IsNullOrEmpty(StreetName1) ? string.Empty : " " + StreetName1.Trim() + ", ");
            }
        }
        public string FullAddressLine4
        {
            get
            {
                return string.IsNullOrEmpty(Estate) ? string.Empty : Estate + ", ";
            }
        }
        public string FullAddressLine5
        {
            get
            {
                return string.IsNullOrEmpty(DistrictName) ? string.Empty : DistrictName + ", ";
            }
        }
        public string FullAddress
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append(FullAddressLine1);
                strBuilder.Append(FullAddressLine2);
                strBuilder.Append(FullAddressLine3);
                strBuilder.Append(FullAddressLine4);
                strBuilder.Append(FullAddressLine5);
                var _addressDetail = strBuilder.ToString();
                _addressDetail = _addressDetail.TrimEnd();
                _addressDetail = _addressDetail[0..^1];
                return _addressDetail;
            }
        }
        public string FullAddressWithBreakLine
        {
            get
            {
                StringBuilder strBuilder = new StringBuilder();

                if (!string.IsNullOrEmpty(FullAddressLine1))
                {
                    strBuilder.AppendLine(FullAddressLine1);
                }

                if (!string.IsNullOrEmpty(FullAddressLine2))
                {
                    strBuilder.AppendLine(FullAddressLine2);
                }

                if (!string.IsNullOrEmpty(FullAddressLine3))
                {
                    strBuilder.AppendLine(FullAddressLine3);
                }

                if (!string.IsNullOrEmpty(FullAddressLine4))
                {
                    strBuilder.AppendLine(FullAddressLine4);
                }

                if (!string.IsNullOrEmpty(FullAddressLine5))
                {
                    strBuilder.AppendLine(FullAddressLine5);
                }

                var _addressDetail = strBuilder.ToString();
                _addressDetail = _addressDetail.TrimEnd();
                _addressDetail = _addressDetail[0..^1];
                return _addressDetail;
            }
        }
        public string AlternateFullAddress
        {
            get
            {
                if (!string.IsNullOrEmpty(StreetNoFrom2) || !string.IsNullOrEmpty(StreetNoTo2) || !string.IsNullOrEmpty(StreetName2))
                {
                    var _addressDetail = string.Empty;
                    _addressDetail += (string.IsNullOrEmpty(UnitText) ? string.Empty : ("Unit " + UnitText.Trim() + ", ")) + (string.IsNullOrEmpty(FloorText) ? string.Empty : ("Floor " + FloorText.Trim() + ", "));
                    _addressDetail += (string.IsNullOrEmpty(Building) ? string.Empty : Building.Trim()) + (string.IsNullOrEmpty(Block) ? ", " : " (Block " + Block.Trim() + ")" + ", ");
                    _addressDetail += (string.IsNullOrEmpty(StreetNoFrom2) ? string.Empty : StreetNoFrom2.Trim()) + (string.IsNullOrEmpty(StreetNoTo2) ? string.Empty : "-" + StreetNoTo2.Trim()) + (string.IsNullOrEmpty(StreetName2) ? string.Empty : " " + StreetName2.Trim() + ", ");
                    _addressDetail += string.IsNullOrEmpty(Estate) ? string.Empty : Estate + ", ";
                    _addressDetail += string.IsNullOrEmpty(DistrictName) ? string.Empty : DistrictName + ", ";
                    _addressDetail = _addressDetail.TrimEnd();
                    _addressDetail = _addressDetail[0..^1];
                    return _addressDetail;
                }
                else
                {
                    return "No Alternate Full Address";
                }
            }
        }
        public string Usage { get; set; }
        public int? Gfa { get; set; }
        public int? Nfa { get; set; }
        public int? BayWindow { get; set; }
        public int? Balcony { get; set; }
        public int? UtilityPlatform { get; set; }
        public int? Acroom { get; set; }
        public int? Acplatform { get; set; }
        public int? FlatRoof { get; set; }
        public int? Roof { get; set; }
        public int? Garden { get; set; }
        public int? Terrace { get; set; }
        public int? Yard { get; set; }
        public int? Cockloft { get; set; }
        public string Dir { get; set; }
        public string Pat { get; set; }
        public string AverageConsideration_MegaUnitFormat { get; set; }
        public string AverageConsideration { get; set; }
        public string ValuationHKD_GFA { get; set; }
        public string ValuationHKD_NFA { get; set; }
        public string ValuationDate { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
