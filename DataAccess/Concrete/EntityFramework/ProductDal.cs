using Identity_Session.Core.DataAccess.EntityFramework;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Identity_Session.DataAccess.Concrete.EntityFramework
{
    public class ProductDal : EntityRepositoryBase<Product, ApplicationDbContext>, IProductDal
    {
        public async Task<bool> Dislike(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var dislike = context.Set<Product>().Where(i => i.Id == id).SingleOrDefault();
                dislike.Dislike++;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Product>().Include("ApplicationUser").Include("Category").Include("Orders").Include("Comments").Include("Pictures").Where(i => i.IsConfirmed == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Product>> GetAllProductsByCateogry(int categoryId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Product>().Include("ApplicationUser").Include("Category").Include("Orders").Include("Comments").Include("Pictures").Where(i => i.IsConfirmed == true && i.IsDeleted == false && i.CategoryId == categoryId).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Product>> GetAllProductsByUserId(string userId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Product>().Include("ApplicationUser").Include("Category").Include("Orders").Include("Comments").Include("Pictures").Where(i => i.IsConfirmed == true && i.IsDeleted == false && i.ApplicationUserId == userId).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public List<Product> GetAllProductsSync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Set<Product>().Include("ApplicationUser").Include("Category").Include("Orders").Include("Comments").Include("Pictures").Where(i => i.IsConfirmed == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToList();
            }
        }

        public async Task<Product> GetProductById(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Product>().Include("ApplicationUser").Include("Category").Include("Orders").Include("Comments").Include("Pictures").Where(i => i.Id == id).FirstOrDefaultAsync();
            }
        }

        public Product HitRead(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var hit = context.Set<Product>().Where(i => i.Id == id).SingleOrDefault();
                hit.Hit++;
                context.SaveChanges();
                return hit;
            }
        }

        public async Task<bool> Like(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var like = context.Set<Product>().Where(i => i.Id == id).SingleOrDefault();
                like.Like++;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Product>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = true;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Product>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = false;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeleted(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var deleted = context.Set<Product>().Where(i => i.Id == id).FirstOrDefault();
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
                var deleted = context.Set<Product>().Where(i => i.Id == id).FirstOrDefault();
                deleted.IsDeleted = false;
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
