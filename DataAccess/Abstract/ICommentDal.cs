using Identity_Session.Core.DataAccess;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.DataAccess.Abstract
{
    public interface ICommentDal : IEntityRepository<Comment>
    {
        Task<List<Comment>> GetAllComments();
        List<Comment>  GetAllCommentsSync();
        Task<List<Comment>> GetAllCommentsByUser(string userId);
        Task<List<Comment>> GetAllCommentsByProduct(int? productId);
        Task<Comment> GetCommentById(int? id);
        Comment HitRead(int? id);       
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
