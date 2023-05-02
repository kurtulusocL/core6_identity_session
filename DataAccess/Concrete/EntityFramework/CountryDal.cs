using Identity_Session.Core.DataAccess.EntityFramework;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Identity_Session.DataAccess.Concrete.EntityFramework
{
    public class CountryDal : EntityRepositoryBase<Country, ApplicationDbContext>, ICountryDal
    {
        public async Task<List<Country>> GetAllCountries()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Country>().Include("Orders").Where(i => i.IsDeleted == false && i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Country>> GetAllCountriesForAdd()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Country>().Include("Orders").Where(i => i.IsDeleted == false && i.IsConfirmed == true).OrderByDescending(i => i.Orders.Count()).ToListAsync();
            }
        }

        public List<Country> GetAllCountriesSync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Set<Country>().Include("Orders").Where(i => i.IsDeleted == false && i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToList();
            }
        }

        public async Task<Country> GetCountryById(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Country>().Include("Orders").Where(i => i.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> SetActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Country>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = true;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Country>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = false;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeleted(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var deleted = context.Set<Country>().Where(i => i.Id == id).FirstOrDefault();
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
                var deleted = context.Set<Country>().Where(i => i.Id == id).FirstOrDefault();
                deleted.IsDeleted = false;
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
