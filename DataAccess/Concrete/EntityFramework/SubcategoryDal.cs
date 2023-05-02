using Identity_Session.Core.DataAccess.EntityFramework;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Identity_Session.DataAccess.Concrete.EntityFramework
{
    public class SubcategoryDal : EntityRepositoryBase<Subcategory, ApplicationDbContext>, ISubcategoryDal
    {
        public async Task<List<Subcategory>> GetAllSubcategories()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Subcategory>().Include("Category").Include("Products").Where(i => i.IsDeleted == false && i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Subcategory>> GetAllSubcategoriesByCategoryId(int? categoryId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Subcategory>().Include("Category").Include("Products").Where(i => i.IsDeleted == false && i.IsConfirmed == true && i.CategoryId == categoryId).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Subcategory>> GetAllSubcategoriesByCategoryIdForAdd(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Subcategory>().Include("Category").Include("Products").Where(i => i.IsDeleted == false && i.IsConfirmed == true && i.CategoryId == id).OrderByDescending(i => i.Products.Count()).ToListAsync();
            }
        }

        public async Task<Subcategory> GetSubcategoryById(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Subcategory>().Include("Category").Include("Products").Where(i => i.Id == id).FirstOrDefaultAsync();
            }
        }
    }
}
