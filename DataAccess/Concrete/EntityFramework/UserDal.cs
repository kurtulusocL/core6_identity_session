using Identity_Session.Core.CrossCuttingConcern.Role.Microsoft;
using Identity_Session.Core.DataAccess.EntityFramework;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Identity_Session.DataAccess.Concrete.EntityFramework
{
    public class UserDal : EntityRepositoryBase<ApplicationUser, ApplicationDbContext>, IUserDal
    {
        public async Task<List<ApplicationUser>> GetAllAdmins()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<ApplicationUser>().Include("Products").Include("Orders").Include("Comments").Include("ProfilePhotos").Include("Pictures").Where(i => i.IsConfirmed == true && i.IsDeleted == false && i.RespondTitle != "Users").OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<ApplicationUser>> GetAllMembers()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<ApplicationUser>().Include("Products").Include("Orders").Include("Comments").Include("ProfilePhotos").Include("Pictures").Where(i => i.IsConfirmed == true && i.IsDeleted == false && i.RespondTitle == "Members").OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<ApplicationUser>().Include("Products").Include("Orders").Include("Comments").Include("ProfilePhotos").Include("Pictures").Where(i => i.IsConfirmed == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<ApplicationUser>().Include("Products").Include("Orders").Include("Comments").Include("ProfilePhotos").Include("Pictures").Where(i => i.Id == id).FirstOrDefaultAsync();
            }
        }

        public ApplicationUser GetUserByIdSync(string userId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Set<ApplicationUser>().Include("Products").Include("Orders").Include("Comments").Include("ProfilePhotos").Include("Pictures").Where(i => i.Id == userId).FirstOrDefault();
            }
        }

        public async Task<bool> SetActive(string id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<ApplicationRole>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = true;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeActive(string id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<ApplicationUser>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = false;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeleted(string id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var deleted = context.Set<ApplicationUser>().Where(i => i.Id == id).FirstOrDefault();
                deleted.IsDeleted = true;
                deleted.DeletedDate = DateTime.Now.ToLocalTime();
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetNotDeleted(string id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var deleted = context.Set<ApplicationUser>().Where(i => i.Id == id).FirstOrDefault();
                deleted.IsDeleted = false;
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
