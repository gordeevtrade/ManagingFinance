using System.ComponentModel.DataAnnotations.Schema;

namespace FamilyBudjetAPI
{
    public class CategoryType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public IEnumerable<Category> subcategories { get; set; }
    }
}