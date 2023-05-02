using Identity_Session.Business.Abstract;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public async Task<bool> Create(Category model)
        {
            await _categoryDal.AddAsync(model);
            return true;
        }

        public async Task<bool> Delete(Category model)
        {
            await _categoryDal.DeleteAsync(model);
            return true;
        }

        public async Task<ICollection<Category>> GetAll()
        {
            return await _categoryDal.GetAllCategories();
        }

        public async Task<List<Category>> GetAllCategoriesForAdd()
        {
            return await _categoryDal.GetAllCategoriesForAdd();
        }

        public async Task<ICollection<Category>> GetAllCategoriesWithoutParameter()
        {
            return await _categoryDal.GetAllCategoriesWithoutParameter();
        }

        public ICollection<Category> GetAllSync()
        {
            return _categoryDal.GetAllCategoriesSync();
        }

        public async Task<Category> GetById(int? id)
        {
            return await _categoryDal.GetCategoryById(id);
        }

        public async Task<bool> SetActive(int id)
        {
            await _categoryDal.SetActive(id);
            return true;
        }

        public async Task<bool> SetDeActive(int id)
        {
            await _categoryDal.SetDeActive(id);
            return true;
        }

        public async Task<bool> SetDeleted(int id)
        {
            await _categoryDal.SetDeleted(id);
            return true;
        }

        public async Task<bool> SetNotDeleted(int id)
        {
            await _categoryDal.SetNotDeleted(id);
            return true;
        }

        public async Task<bool> Update(Category model)
        {
            model.UpdatedDate = DateTime.Now.ToLocalTime();
            await _categoryDal.UpdateAsync(model);
            return true;
        }
    }
}
