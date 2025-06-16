using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Requests.MemfusWongData
{
    public class ZoneRequest : SearchRequest
    {
        public ZoneRequest(int? start, int? length)
            : base(start, length) { }
    }
}
