using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Requests.MemfusWongData
{
    public class DistrictRequest : SearchRequest
    {
        public DistrictRequest(int? start, int? length)
            : base(start, length) { }

        public string ZoneId { get; set; }
        public string DistrictId { get; set; }
    }
}
