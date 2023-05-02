using Identity_Session.Core.DataAccess;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        Task<List<Product>> GetAllProducts();
        List<Product> GetAllProductsSync();
        Task<List<Product>> GetAllProductsByUserId(string userId);
        Task<List<Product>> GetAllProductsByCateogry(int categoryId);
        Task<Product> GetProductById(int? id);
        Product HitRead(int? id);
        Task<bool> Like(int? id);
        Task<bool> Dislike(int? id);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
