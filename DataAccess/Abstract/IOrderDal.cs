using Identity_Session.Core.DataAccess;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepository<Order>
    {
        Task<List<Order>> GetAllOrders();
        List<Order> GetAllOrdersSync();
        Task<List<Order>> GetAllSentOrders();
        Task<List<Order>> GetAllOrdersByUserId(string userId);
        Task<List<Order>> GetAllOrdersByProduct(int? productId);
        Task<List<Order>> GetAllOrdersByCountry(int? countryId);
        Task<Order> GetOrderById(int? id);
        Task<bool> SetSend(int id);
        Task<bool> SetNotSend(int id);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
