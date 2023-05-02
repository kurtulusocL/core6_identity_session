using Identity_Session.Core.DataAccess.EntityFramework;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.DataAccess.Concrete.EntityFramework.Context;
using Identity_Session.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Identity_Session.DataAccess.Concrete.EntityFramework
{
    public class CommentDal : EntityRepositoryBase<Comment, ApplicationDbContext>, ICommentDal
    {
        public async Task<List<Comment>> GetAllComments()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Comment>().Include("ApplicationUser").Include("Product").Where(i => i.IsConfirmed == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<List<Comment>> GetAllCommentsByProduct(int? productId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Comment>().Include("ApplicationUser").Include("Product").Where(i => i.IsConfirmed == true && i.IsDeleted == false && i.ProductId == productId).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public async Task<Comment> GetCommentById(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Comment>().Include("ApplicationUser").Include("Product").Where(i => i.Id == id).FirstOrDefaultAsync();
            }
        }

        public async Task<List<Comment>> GetAllCommentsByUser(string userId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<Comment>().Include("ApplicationUser").Include("Product").Where(i => i.IsConfirmed == true && i.IsDeleted == false && i.ApplicationUserId == userId).OrderByDescending(i => i.CreatedDate).ToListAsync();
            }
        }

        public Comment HitRead(int? id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var hit = context.Set<Comment>().Where(i => i.Id == id).SingleOrDefault();
                hit.Hit++;
                context.SaveChanges();
                return hit;
            }
        }

        public async Task<bool> SetActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Comment>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = true;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeActive(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var active = context.Set<Comment>().Where(i => i.Id == id).FirstOrDefault();
                active.IsConfirmed = false;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> SetDeleted(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var deleted = context.Set<Comment>().Where(i => i.Id == id).FirstOrDefault();
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
                var deleted = context.Set<Comment>().Where(i => i.Id == id).FirstOrDefault();
                deleted.IsDeleted = false;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public List<Comment> GetAllCommentsSync()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Set<Comment>().Include("ApplicationUser").Include("Product").Where(i => i.IsConfirmed == true && i.IsDeleted == false).OrderByDescending(i => i.CreatedDate).ToList();
            }
        }
    }
}
