using FamilyBudjetAPI;

namespace Budget.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly FinanceContext _context;

        public CategoryRepository(FinanceContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category Get(int id)
        {
            return _context.Categories.Find(id);
        }

        public Category Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public IEnumerable<Category> GetByCategoryType(int categoryTypeId)
        {
            var categories = _context.Categories
                    .Where(c => c.CategoryTypeId == categoryTypeId)
                    .ToList();
            return categories;
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public int CountByCategory(int categoryId)
        {
            return _context.Categories.Count(sc => sc.CategoryTypeId == categoryId);
        }
    }
}