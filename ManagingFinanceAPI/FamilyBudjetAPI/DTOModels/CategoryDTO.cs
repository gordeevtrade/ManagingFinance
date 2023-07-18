namespace FamilyBudjetAPI.DTOModels
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryTypeId { get; set; }
        public int TransactionsCount { get; set; }
    }
}