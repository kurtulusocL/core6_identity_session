using Identity_Session.Business.Abstract;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        readonly IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public async Task<bool> Create(Order model)
        {
            await _orderDal.AddAsync(model);
            return true;
        }

        public async Task<bool> Delete(Order model)
        {
            await _orderDal.DeleteAsync(model);
            return true;
        }

        public async Task<ICollection<Order>> GetAll()
        {
            return await _orderDal.GetAllOrders();
        }

        public async Task<ICollection<Order>> GetAllOrdersByCountry(int? countryId)
        {
            return await _orderDal.GetAllOrdersByCountry(countryId);
        }

        public async Task<ICollection<Order>> GetAllOrdersByProduct(int? productId)
        {
            return await _orderDal.GetAllOrdersByProduct(productId);
        }

        public async Task<ICollection<Order>> GetAllOrdersByUserId(string userId)
        {
            return await _orderDal.GetAllOrdersByUserId(userId);
        }

        public async Task<ICollection<Order>> GetAllSentOrders()
        {
            return await _orderDal.GetAllSentOrders();
        }

        public ICollection<Order> GetAllSync()
        {
            return _orderDal.GetAllOrdersSync();
        }

        public async Task<Order> GetById(int? id)
        {
            return await _orderDal.GetOrderById(id);
        }

        public async Task<bool> SetActive(int id)
        {
            await _orderDal.SetActive(id);
            return true;
        }

        public async Task<bool> SetDeActive(int id)
        {
            await _orderDal.SetDeActive(id);
            return true;
        }

        public async Task<bool> SetDeleted(int id)
        {
            await _orderDal.SetDeleted(id);
            return true;
        }

        public async Task<bool> SetNotDeleted(int id)
        {
            await _orderDal.SetNotDeleted(id);
            return true;
        }

        public async Task<bool> SetNotSend(int id)
        {
            await _orderDal.SetNotSend(id);
            return true;
        }

        public async Task<bool> SetSend(int id)
        {
            await _orderDal.SetSend(id);
            return true;
        }

        public async Task<bool> Update(Order model)
        {
            model.UpdatedDate = DateTime.Now.ToLocalTime();
            await _orderDal.UpdateAsync(model);
            return true;
        }
    }
}
