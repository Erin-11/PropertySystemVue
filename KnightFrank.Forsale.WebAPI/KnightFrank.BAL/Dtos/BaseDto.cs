using System;

namespace KnightFrank.BAL.Dtos
{
    public class BaseDto
    {
        public string IsActive { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
