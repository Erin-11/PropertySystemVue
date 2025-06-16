using KnightFrank.MemfusWongData.Api.Helpers.Datatable.Core.NameConvention;

namespace KnightFrank.MemfusWongData.Api.Helpers.Datatable.NameConvention
{
    /// <summary>
    /// Represents CamelCase response naming convention for DataTables.AspNet.AspNetCore.
    /// </summary>
    public class CamelCaseResponseNameConvention : IResponseNameConvention
    {
        public string Draw { get { return "draw"; } }
        public string TotalRecords { get { return "recordsTotal"; } }
        public string TotalRecordsFiltered { get { return "recordsFiltered"; } }
        public string Data { get { return "data"; } }
        public string Error { get { return "error"; } }
    }
}
