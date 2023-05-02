using Identity_Session.Core.DataAccess;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.DataAccess.Abstract
{
    public interface ISubcategoryDal:IEntityRepository<Subcategory>
    {
        Task<List<Subcategory>> GetAllSubcategories();
        Task<List<Subcategory>> GetAllSubcategoriesByCategoryId(int? categoryId);
        Task<List<Subcategory>> GetAllSubcategoriesByCategoryIdForAdd(int? id);
        Task<Subcategory> GetSubcategoryById(int? id);

    }
}
