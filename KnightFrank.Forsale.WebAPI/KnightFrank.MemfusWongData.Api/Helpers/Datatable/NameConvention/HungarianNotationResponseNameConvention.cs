using KnightFrank.MemfusWongData.Api.Helpers.Datatable.Core.NameConvention;

namespace KnightFrank.MemfusWongData.Api.Helpers.Datatable.NameConvention
{
    /// <summary>
    /// Represents HungarianNotation response naming convention for DataTables.AspNet.AspNetCore.
    /// </summary>
    public class HungarianNotationResponseNameConvention : IResponseNameConvention
    {
        public string Draw { get { return "sEcho"; } }
        public string TotalRecords { get { return "iTotalRecords"; } }
        public string TotalRecordsFiltered { get { return "iTotalDisplayRecords"; } }
        public string Data { get { return "aaData"; } }
        public string Error { get { return string.Empty; } }
    }
}
