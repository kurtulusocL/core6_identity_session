using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Abstract
{
    public interface ICategoryService
    {
        Task<ICollection<Category>> GetAll();
        ICollection<Category> GetAllSync();
        Task<ICollection<Category>> GetAllCategoriesWithoutParameter();
        Task<List<Category>> GetAllCategoriesForAdd();
        Task<Category> GetById(int? id);
        Task<bool> Create(Category model);
        Task<bool> Update(Category model);
        Task<bool> Delete(Category model);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
