using Domain.BL.Models;

namespace FamilyBudjetAPI.Sevices.Interface
{
    public interface ICategoryTypeService
    {
        IEnumerable<CategoryType> GetCategoryTypes();

        CategoryType CreateCategoryType(CategoryType categoryType);

        CategoryType GetCategoryType(int id);

        void UpdateCategoryType(int id, CategoryType updatedCategoryType);

        void DeleteCategoryType(int id);

        public int GetIncomeSubcategoriesCount();

        public int GetExpenseSubcategoriesCount();

        public CategoryTypeCollection ReturnCategoryTypeCollection();
    }
}