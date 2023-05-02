using Identity_Session.Business.Abstract;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Concrete
{
    public class SubcategoryManager:ISubcategoryService
    {
        readonly ISubcategoryDal _subcategoryDal;
        public SubcategoryManager(ISubcategoryDal subcategoryDal)
        {
            _subcategoryDal = subcategoryDal;
        }

        public async Task<bool> Create(Subcategory model)
        {
            await _subcategoryDal.AddAsync(model);
            return true;
        }

        public async Task<bool> Delete(Subcategory model)
        {
            await _subcategoryDal.DeleteAsync(model);
            return true;
        }

        public async Task<ICollection<Subcategory>> GetAll()
        {
            return await _subcategoryDal.GetAllSubcategories();
        }

        public async Task<ICollection<Subcategory>> GetAllSubcategoriesByCategoryId(int? categoryId)
        {
            return await _subcategoryDal.GetAllSubcategoriesByCategoryId(categoryId);
        }

        public async Task<ICollection<Subcategory>> GetAllSubcategoriesByCategoryIdForAdd(int? id)
        {
            return await _subcategoryDal.GetAllSubcategoriesByCategoryIdForAdd(id);
        }

        public async Task<Subcategory> GetById(int? id)
        {
            return await _subcategoryDal.GetSubcategoryById(id);
        }

        public async Task<bool> Update(Subcategory model)
        {
            model.UpdatedDate = DateTime.Now.ToLocalTime();
            await _subcategoryDal.UpdateAsync(model);
            return true;
        }
    }
}
