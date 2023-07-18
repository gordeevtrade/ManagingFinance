using Budget.DAL.Repositories.Interfaces;
using FamilyBudjetAPI;

namespace Budget.DAL.Repositories
{
    public class CategoryTypeRepository : ICategoryTypeRepository
    {
        protected readonly FinanceContext _context;

        public CategoryTypeRepository(FinanceContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoryType> GetAll()
        {
            return _context.CategoryTypes.ToList();
        }

        public CategoryType Get(int id)
        {
            return _context.CategoryTypes.Find(id);
        }

        public CategoryType Create(CategoryType categoryType)
        {
            _context.CategoryTypes.Add(categoryType);
            _context.SaveChanges();
            return categoryType;
        }

        public void Update(CategoryType categoryType)
        {
            _context.Update(categoryType);
            _context.SaveChanges();
        }

        public void Delete(CategoryType categoryType)
        {
            _context.CategoryTypes.Remove(categoryType);
            _context.SaveChanges();
        }
    }
}