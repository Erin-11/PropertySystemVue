using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Requests.MemfusWongData
{
    public class StreetRequest : SearchRequest
    {
        public StreetRequest(int? start, int? length)
            : base(start, length) { }

        public string ZoneId { get; set; }
        public string DistrictId { get; set; }
        public Guid? StreetID { get; set; }
        public string StreetNumberFrom { get; set; }
        public string StreetNumberTo { get; set; }
        public Guid? EstateID { get; set; }
        public Guid? BuildingID { get; set; }
    }
}
