using KnightFrank.DataAccessLayer.EF.Common;

namespace KnightFrank.DAL.Entities.Base
{
    public partial class ModelEntityBase : EntityBase
    {
        public new virtual bool IsActive { get; set; }
    }
}
