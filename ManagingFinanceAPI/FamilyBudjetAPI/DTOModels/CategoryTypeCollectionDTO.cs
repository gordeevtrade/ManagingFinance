using FamilyBudjetAPI.DTOModels;

namespace ManagingFinanceAPI.DTOModels
{
    public class CategoryTypeCollectionDTO
    {
        public IEnumerable<CategoryTypeDTO> Categories { get; set; }
    }
}