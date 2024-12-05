using ToDo.API.Domain.Entities;

namespace ToDo.API.Domain.Core.Specifications
{
    public class MissionSpecification
    {
        public static BaseSpecification<MissionItem> GetMisshionByCategoryId(int categoryId)
        {
            return new BaseSpecification<MissionItem>(mi => mi.CategoryId == categoryId);
        }
    }
}
