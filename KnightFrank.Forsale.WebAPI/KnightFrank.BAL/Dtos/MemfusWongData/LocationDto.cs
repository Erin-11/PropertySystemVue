using KnightFrank.BAL.Attributes;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace KnightFrank.BAL.Dtos.MemfusWongData
{
    public partial class LocationDto
    {
        [IgnoreDataMember]
        public District District { get; set; }
        [IgnoreDataMember]
        public Street Street1 { get; set; }
        [IgnoreDataMember]
        public Street Street2 { get; set; }
        [IgnoreDataMember]
        public Estate Estate { get; set; }
        [IgnoreDataMember]
        public Building Building { get; set; }

        [Key]
        [KeywordSearch(false)]
        public Guid LocationId { get; set; }
        public string ZoneId
        {
            get
            {
                return District?.Zone?.ZoneId ?? string.Empty;
            }
        }
        public string ZoneName
        {
            get
            {
                return District?.Zone?.ZoneName ?? string.Empty;
            }
        }
        public string ZoneNameChin
        {
            get
            {
                return District?.Zone?.ZoneNameChin ?? string.Empty;
            }
        }
        public string DistrictId { get; set; }
        public string DistrictName
        {
            get
            {
                return District?.DistrictName ?? string.Empty;
            }
        }
        public string DistrictNameChin
        {
            get
            {
                return District?.DistrictNameChin ?? string.Empty;
            }
        }
        public Guid? BuildingId { get; set; }
        public string BuildingName
        {
            get
            {
                return Building?.BuildingName ?? string.Empty;
            }
        }
        public string BuildingNameChin
        {
            get
            {
                return Building?.BuildingNameChin ?? string.Empty;
            }
        }
        public Guid? EstateId { get; set; }
        public string EstateName
        {
            get
            {
                return Estate?.EstateName ?? string.Empty;
            }
        }
        public string EstateNameChin
        {
            get
            {
                return Estate?.EstateNameChin ?? string.Empty;
            }
        }
        public Guid? Street1Id { get; set; }
        public string StreetName1
        {
            get
            {
                return Street1?.StreetName ?? string.Empty;
            }
        }
        public string StreetNameChin1
        {
            get
            {
                return Street1?.StreetNameChin ?? string.Empty;
            }
        }
        public Guid? Street2Id { get; set; }
        public string StreetName2
        {
            get
            {
                return Street2?.StreetName ?? string.Empty;
            }
        }
        public string StreetNameChin2
        {
            get
            {
                return Street2?.StreetNameChin ?? string.Empty;
            }
        }
        public string StreetNumberFrom1 { get; set; }
        public string StreetNumberTo1 { get; set; }
        public string StreetNumberFrom2 { get; set; }
        public string StreetNumberTo2 { get; set; }

        //public string CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public string ModifiedBy { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public bool IsActive { get; set; }
    }
}
