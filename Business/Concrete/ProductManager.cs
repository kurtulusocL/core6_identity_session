using Identity_Session.Business.Abstract;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Concrete
{
    public class ProductManager : IProductService
    {
        readonly IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public async Task<bool> Create(Product model)
        {
            await _productDal.AddAsync(model);
            return true;
        }

        public async Task<bool> Delete(Product model)
        {
            await _productDal.DeleteAsync(model);
            return true;
        }

        public async Task<bool> Dislike(int? id)
        {
            await _productDal.Dislike(id);
            return true;
        }

        public async Task<ICollection<Product>> GetAll()
        {
            return await _productDal.GetAllProducts();
        }

        public async Task<ICollection<Product>> GetAllProductsByCateogry(int categoryId)
        {
            return await _productDal.GetAllProductsByCateogry(categoryId);
        }

        public async Task<ICollection<Product>> GetAllProductsByUserId(string userId)
        {
            return await _productDal.GetAllProductsByUserId(userId);
        }

        public ICollection<Product> GetAllSync()
        {
            return _productDal.GetAllProductsSync();
        }

        public async Task<Product> GetById(int? id)
        {
            return await _productDal.GetProductById(id);
        }

        public Product HitRead(int? id)
        {
            return _productDal.HitRead(id);
        }

        public async Task<bool> Like(int? id)
        {
            await _productDal.Like(id);
            return true;
        }

        public async Task<bool> SetActive(int id)
        {
            await _productDal.SetActive(id);
            return true;
        }

        public async Task<bool> SetDeActive(int id)
        {
            await _productDal.SetDeActive(id);
            return true;
        }

        public async Task<bool> SetDeleted(int id)
        {
            await _productDal.SetDeleted(id);
            return true;
        }

        public async Task<bool> SetNotDeleted(int id)
        {
            await _productDal.SetNotDeleted(id);
            return true;
        }

        public async Task<bool> Update(Product model)
        {
            model.UpdatedDate = DateTime.Now.ToLocalTime();
            await _productDal.UpdateAsync(model);
            return true;
        }
    }
}
