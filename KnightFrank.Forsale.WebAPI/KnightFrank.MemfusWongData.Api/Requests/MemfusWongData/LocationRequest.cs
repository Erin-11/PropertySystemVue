using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Requests.MemfusWongData
{
    public class LocationRequest : SearchRequest
    {
        public LocationRequest(int? start, int? length)
            : base(start, length) { }

        public string ZoneID { get; set; }
        public string DistrictID { get; set; }
        public string StreetKeyword { get; set; }
        //public int? FilterStreetNumberFrom { get; set; }
        //public int? FilterStreetNumberTo { get; set; }
        public string EstateKeyword { get; set; }
        public string BuildingKeyword { get; set; }
    }
}
