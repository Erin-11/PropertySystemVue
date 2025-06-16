using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Requests.MemfusWongData
{
    public class FloorRequest : SearchRequest
    {
        public FloorRequest(int? start, int? length)
            : base(start, length) { }

        public string ZoneID { get; set; }
        public string DistrictID { get; set; }
        public Guid? StreetID { get; set; }
        public Guid? EstateID { get; set; }
        public Guid? BuildingID { get; set; }
        public int? PhaseID { get; set; }
        public int? BlockID { get; set; }
        public int? FloorID { get; set; }
    }
}
