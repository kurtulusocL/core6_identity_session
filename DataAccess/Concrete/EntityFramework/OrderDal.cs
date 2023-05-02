using Identity_Session.Core.DataAccess.EntityFramework;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Identity_Session.DataAccess.Concrete.EntityFramework
{
    public class OrderDal : EntityRepositoryBase<Order, ApplicationDbContext>, IOrderDal
    {
        public async Task<List<Order>> GetAllOrders()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Order>().Include("ApplicationUser").Include("Product").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Order>> GetAllOrdersByCountry(int? countryId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Order>().Include("ApplicationUser").Include("Product").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirmed == true && i.CountryId == countryId).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Order>> GetAllOrdersByProduct(int? productId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Order>().Include("ApplicationUser").Include("Product").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirmed == true && i.ProductId == productId).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Order>> GetAllOrdersByUserId(string userId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Order>().Include("ApplicationUser").Include("Product").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirmed == true && i.ApplicationUserId == userId).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public List<Order> GetAllOrdersSync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Set<Order>().Include("ApplicationUser").Include("Product").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirmed == true).OrderByDescending(i => i.CreatedDate).ToList();
            }
        }

        public async Task<List<Order>> GetAllSentOrders()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Order>().Include("ApplicationUser").Include("Product").Include("Country").Where(i => i.IsDeleted == false && i.IsConfirmed == true && i.IsSend == true).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<Order> GetOrderById(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Order>().Include("ApplicationUser").Include("Product").Include("Country").Where(i => i.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> SetActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Order>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = true;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Order>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = false;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeleted(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var deleted = context.Set<Order>().Where(i => i.Id == id).FirstOrDefault();
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
                var deleted = context.Set<Order>().Where(i => i.Id == id).FirstOrDefault();
                deleted.IsDeleted = false;
                await context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> SetNotSend(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var sent = context.Set<Order>().Where(i => i.Id == id).FirstOrDefault();
                sent.IsSend = false;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetSend(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var sent = context.Set<Order>().Where(i => i.Id == id).FirstOrDefault();
                sent.IsSend = true;
                sent.SendDate = DateTime.Now.ToLocalTime();
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
