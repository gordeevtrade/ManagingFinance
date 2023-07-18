namespace FamilyBudjetAPI.Sevices.Interface
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetCategories();

        Category CreateCategory(Category category);

        Category GetCategory(int id);

        IEnumerable<Category> GetCategoriesByCategoryType(int categoryTypeId);

        void UpdateCategory(Category updatedCategory);

        void DeleteCategory(int id);
    }
}