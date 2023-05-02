using Identity_Session.Core.DataAccess;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.DataAccess.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {
        Task<List<Category>> GetAllCategories();
        List<Category> GetAllCategoriesSync();
        Task<List<Category>> GetAllCategoriesWithoutParameter();
        Task<List<Category>> GetAllCategoriesForAdd();
        Task<Category> GetCategoryById(int? id);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
