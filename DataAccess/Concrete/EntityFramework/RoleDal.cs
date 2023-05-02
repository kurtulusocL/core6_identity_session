using Identity_Session.Core.CrossCuttingConcern.Role.Microsoft;
using Identity_Session.Core.DataAccess.EntityFramework;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.DataAccess.Concrete.EntityFramework
{
    public class RoleDal : EntityRepositoryBase<ApplicationRole, ApplicationDbContext>, IRoleDal
    {
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
                var active = context.Set<ApplicationRole>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = false;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeleted(string id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var deleted = context.Set<ApplicationRole>().Where(i => i.Id == id).FirstOrDefault();
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
                var deleted = context.Set<ApplicationRole>().Where(i => i.Id == id).FirstOrDefault();
                deleted.IsDeleted = false;
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
