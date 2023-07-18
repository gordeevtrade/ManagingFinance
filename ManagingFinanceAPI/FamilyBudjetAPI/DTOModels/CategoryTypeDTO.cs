namespace FamilyBudjetAPI.DTOModels
{
    public class CategoryTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoryDTO> subcategories { get; set; }
    }
}