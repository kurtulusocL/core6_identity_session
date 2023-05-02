using Identity_Session.Core.DataAccess.EntityFramework;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Identity_Session.DataAccess.Concrete.EntityFramework
{
    public class PictureDal : EntityRepositoryBase<Picture, ApplicationDbContext>, IPictureDal
    {
        public async Task<List<Picture>> GetAllPictures()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Picture>().Include("Product").Where(i => i.IsDeleted == false && i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Picture>> GetAllPicturesByProductId(int? productId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Picture>().Include("Product").Where(i => i.IsDeleted == false && i.IsConfirmed == true && i.ProductId == productId).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Picture>> GetAllPicturesByUserId(string userId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Picture>().Include("Product").Where(i => i.IsDeleted == false && i.IsConfirmed == true && i.ApplicationUserId == userId).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public List<Picture> GetAllPicturesSync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Set<Picture>().Include("Product").Where(i => i.IsDeleted == false && i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToList();
            }
        }

        public async Task<Picture> GetPictureById(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Picture>().Include("Product").Where(i => i.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<Picture> GetPictureByProductId(int? productId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Picture>().Include("Product").Where(i => i.ProductId == productId).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> SetActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Picture>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = true;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Picture>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = false;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeleted(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var deleted = context.Set<Picture>().Where(i => i.Id == id).FirstOrDefault();
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
                var deleted = context.Set<Picture>().Where(i => i.Id == id).FirstOrDefault();
                deleted.IsDeleted = false;
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
