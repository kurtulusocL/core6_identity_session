using Identity_Session.Core.DataAccess.EntityFramework;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Identity_Session.DataAccess.Concrete.EntityFramework
{
    public class ProfilePhotoDal : EntityRepositoryBase<ProfilePhoto, ApplicationDbContext>, IProfilePhotoDal
    {
        public async Task<List<ProfilePhoto>> GetAllProfilePhotoByUserId(string userId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<ProfilePhoto>().Include("ApplicationUser").Where(i => i.IsConfirmed == true && i.IsDeleted == false && i.ApplicationUserId == userId).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<ProfilePhoto>> GetAllProfilePhotos()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<ProfilePhoto>().Include("ApplicationUser").Where(i => i.IsConfirmed == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<ProfilePhoto> GetProfilePhotoById(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<ProfilePhoto>().Include("ApplicationUser").Where(i => i.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<ProfilePhoto> GetProfilePhotoByUserId(string userId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<ProfilePhoto>().Include("ApplicationUser").Where(i => i.ApplicationUserId == userId).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> SetActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<ProfilePhoto>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = true;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<ProfilePhoto>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = false;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeleted(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var deleted = context.Set<ProfilePhoto>().Where(i => i.Id == id).FirstOrDefault();
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
                var deleted = context.Set<ProfilePhoto>().Where(i => i.Id == id).FirstOrDefault();
                deleted.IsDeleted = false;
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
