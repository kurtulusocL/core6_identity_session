using Identity_Session.Business.Abstract;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Concrete
{
    public class CommentManager : ICommentService
    {
        readonly ICommentDal _commentDal;
        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }
        public async Task<bool> Create(Comment model)
        {
            await _commentDal.AddAsync(model);
            return true;
        }

        public async Task<bool> Delete(Comment model)
        {
            await _commentDal.DeleteAsync(model);
            return true;
        }

        public async Task<ICollection<Comment>> GetAll()
        {
            return await _commentDal.GetAllComments();
        }

        public async Task<ICollection<Comment>> GetAllCommentsByProduct(int? productId)
        {
            return await _commentDal.GetAllCommentsByProduct(productId);
        }

        public async Task<ICollection<Comment>> GetAllCommentsByUser(string userId)
        {
            return await _commentDal.GetAllCommentsByUser(userId);
        }

        public ICollection<Comment> GetAllSync()
        {
            return _commentDal.GetAllCommentsSync();
        }

        public async Task<Comment> GetById(int? id)
        {
            return await _commentDal.GetCommentById(id);
        }

        public Comment HitRead(int? id)
        {
            return _commentDal.HitRead(id);
        }

        public async Task<bool> SetActive(int id)
        {
            await _commentDal.SetActive(id);
            return true;
        }

        public async Task<bool> SetDeActive(int id)
        {
            await _commentDal.SetDeActive(id);
            return true;
        }

        public async Task<bool> SetDeleted(int id)
        {
            await _commentDal.SetDeleted(id);
            return true;
        }

        public async Task<bool> SetNotDeleted(int id)
        {
            await _commentDal.SetNotDeleted(id);
            return true;
        }
    }
}
