using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyBudjetAPI
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public int TransactionsCount { get; set; }
        public int CategoryTypeId { get; set; }
        public CategoryType? CategoryType { get; set; }
    }
}