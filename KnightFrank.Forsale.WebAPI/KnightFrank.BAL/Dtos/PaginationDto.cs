using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnightFrank.BAL.Dtos
{
    public class PaginationDto<TDto> where TDto : class
    {
        public PaginationDto()
        {
            DataObjects = new List<TDto>();
        }

        public PaginationDto(int totalRecords, int pageSize, int pageNumber, IEnumerable<TDto> dataObjects)
        {
            PageSize = pageSize;
            TotalRecords = totalRecords;
            PageNumber = pageNumber;
            DataObjects = dataObjects;
        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        [Display(Name = "iTotalRecords")]
        public int TotalRecords { get; set; }
        public IEnumerable<TDto> DataObjects { get; set; }
    }
}
