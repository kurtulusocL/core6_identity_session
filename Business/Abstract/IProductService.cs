using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Abstract
{
    public interface IProductService
    {
        Task<ICollection<Product>> GetAll();
        ICollection<Product> GetAllSync();
        Task<ICollection<Product>> GetAllProductsByUserId(string userId);
        Task<ICollection<Product>> GetAllProductsByCateogry(int categoryId);
        Task<Product> GetById(int? id);
        Product HitRead(int? id);
        Task<bool> Like(int? id);
        Task<bool> Dislike(int? id);
        Task<bool> Create(Product model);
        Task<bool> Update(Product model);
        Task<bool> Delete(Product model);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
