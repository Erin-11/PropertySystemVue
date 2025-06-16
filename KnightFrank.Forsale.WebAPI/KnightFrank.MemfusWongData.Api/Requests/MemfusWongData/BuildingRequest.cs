using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Requests.MemfusWongData
{
    public class BuildingRequest : SearchRequest
    {
        public BuildingRequest(int? start, int? length)
            : base(start, length) { }

        public string ZoneID { get; set; }
        public string DistrictID { get; set; }
        public Guid? StreetID { get; set; }
        public string StreetNumberFrom { get; set; }
        public string StreetNumberTo { get; set; }
        public Guid? EstateID { get; set; }
        public Guid? BuildingID { get; set; }
    }
}
