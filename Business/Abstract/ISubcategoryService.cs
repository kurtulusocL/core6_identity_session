using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Abstract
{
    public interface ISubcategoryService
    {
        Task<ICollection<Subcategory>> GetAll();
        Task<ICollection<Subcategory>> GetAllSubcategoriesByCategoryId(int? categoryId);
        Task<ICollection<Subcategory>> GetAllSubcategoriesByCategoryIdForAdd(int? id);
        Task<Subcategory> GetById(int? id);
        Task<bool> Create(Subcategory model);
        Task<bool> Update(Subcategory model);
        Task<bool> Delete(Subcategory model);
    }
}
