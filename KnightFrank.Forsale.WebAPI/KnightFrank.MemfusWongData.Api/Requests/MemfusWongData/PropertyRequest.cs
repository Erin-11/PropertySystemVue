using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Requests.MemfusWongData
{
    public class PropertyRequest : SearchRequest
    {
        public PropertyRequest(int? start, int? length)
            : base(start, length) { }
    }
}
