﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnightFrank.MemfusWongData.Api.Requests.MemfusWongData
{
    public class PropertySearchRequest
    {
        public string? Region { get; set; }
        public string? District { get; set; }
        public List<string>? PropertyTypes { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 9;
    }
}
