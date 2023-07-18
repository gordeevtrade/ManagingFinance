using FamilyBudjetAPI;

public interface ICategoryRepository
{
    IEnumerable<Category> GetAll();

    Category Get(int id);

    Category Create(Category category);

    IEnumerable<Category> GetByCategoryType(int categoryTypeId);

    void Update(Category category);

    void Delete(Category category);

    int CountByCategory(int categoryId);
}