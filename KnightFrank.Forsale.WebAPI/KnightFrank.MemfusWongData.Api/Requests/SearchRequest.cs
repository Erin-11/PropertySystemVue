using System.Collections.Generic;

namespace KnightFrank.MemfusWongData.Api.Requests
{
    public partial class SearchRequest : BaseRequest
    {
        public SearchRequest(int? start, int? length)
        {
            Start = start;
            Length = length;
        }

        public int? Start { get; set; }
        public int? Length { get; set; }
    }
}
