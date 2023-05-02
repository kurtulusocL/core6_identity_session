using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Abstract
{
    public interface ICommentService
    {
        Task<ICollection<Comment>> GetAll();
        ICollection<Comment> GetAllSync();
        Task<ICollection<Comment>> GetAllCommentsByUser(string userId);
        Task<ICollection<Comment>> GetAllCommentsByProduct(int? productId);
        Task<Comment> GetById(int? id);
        Comment HitRead(int? id);
        Task<bool> Create(Comment model);
        Task<bool> Delete(Comment model);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
