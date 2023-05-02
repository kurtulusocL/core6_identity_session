using Identity_Session.Core.DataAccess.EntityFramework;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Identity_Session.DataAccess.Concrete.EntityFramework
{
    public class CategoryDal : EntityRepositoryBase<Category, ApplicationDbContext>, ICategoryDal
    {
        public async Task<List<Category>> GetAllCategories()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Category>().Include("Products").Where(i => i.IsConfirmed == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Category>> GetAllCategoriesForAdd()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Category>().Include("Products").Where(i => i.IsConfirmed == true && i.IsDeleted == false).OrderByDescending(i => i.Products.Count()).ToListAsync();
            }
        }

        public List<Category> GetAllCategoriesSync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Set<Category>().Include("Products").Where(i => i.IsConfirmed == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToList();
            }
        }

        public async Task<List<Category>> GetAllCategoriesWithoutParameter()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Category>().Include("Products").OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<Category> GetCategoryById(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Category>().Include("Products").Where(i => i.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> SetActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Category>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = true;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Category>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = false;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeleted(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var deleted = context.Set<Category>().Where(i => i.Id == id).FirstOrDefault();
                deleted.IsDeleted = true;
                deleted.DeletedDate = DateTime.Now.ToLocalTime();
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetNotDeleted(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var deleted = context.Set<Category>().Where(i => i.Id == id).FirstOrDefault();
                deleted.IsDeleted = false;
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
