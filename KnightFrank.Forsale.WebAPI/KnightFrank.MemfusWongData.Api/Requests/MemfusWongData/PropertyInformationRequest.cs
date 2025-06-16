using System;
using System.Collections.Generic;

namespace KnightFrank.MemfusWongData.Api.Requests.MemfusWongData
{
    public class PropertyInformationRequest : SearchRequest
    {
        public PropertyInformationRequest(int? start, int? length)
            : base(start, length) { }

        public string ZoneID { get; set; }
        public string DistrictID { get; set; }
        public Guid? StreetID { get; set; }
        public int? FilterStreetNumberFrom { get; set; }
        public int? FilterStreetNumberTo { get; set; }
        public Guid? EstateID { get; set; }
        public Guid? BuildingID { get; set; }
        public int? PhaseID { get; set; }
        public int? BlockID { get; set; }
        public int? FloorID { get; set; }
        public int? UnitID { get; set; }
    }
}
