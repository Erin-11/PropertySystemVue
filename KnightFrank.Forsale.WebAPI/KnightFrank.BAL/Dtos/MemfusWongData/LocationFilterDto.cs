using KnightFrank.BAL.Attributes;
using KnightFrank.DAL.Entities.Models.MemfusWongData;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace KnightFrank.BAL.Dtos.MemfusWongData
{
    public partial class LocationFilterDto
    {
        public string ZoneId { get; set; }
        public string ZoneName { get; set; }
        //public string ZoneNameChin { get; set; }
        public string DistrictId { get; set; }
        public string DistrictName { get; set; }
        //public string DistrictNameChin { get; set; }
        public Guid? Street1Id { get; set; }
        public string StreetName1 { get; set; }
        //public string StreetNameChin1 { get; set; }
        public Guid? Street2Id { get; set; }
        public string StreetName2 { get; set; }
        //public string StreetNameChin2 { get; set; }
        public Guid? EstateId { get; set; }
        public string EstateName { get; set; }
        //public string EstateNameChin { get; set; }
        public Guid? BuildingId { get; set; }
        public string BuildingName { get; set; }
        //public string BuildingNameChin { get; set; }
    }
}
