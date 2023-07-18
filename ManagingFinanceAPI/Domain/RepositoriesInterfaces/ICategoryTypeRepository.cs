using FamilyBudjetAPI;

namespace Budget.DAL.Repositories.Interfaces
{
    public interface ICategoryTypeRepository
    {
        IEnumerable<CategoryType> GetAll();

        CategoryType Get(int id);

        CategoryType Create(CategoryType categoryType);

        void Update(CategoryType categoryType);

        void Delete(CategoryType categoryType);
    }
}