using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Abstract
{
    public interface IOrderService
    {
        Task<ICollection<Order>> GetAll();
        ICollection<Order> GetAllSync();
        Task<ICollection<Order>> GetAllSentOrders();
        Task<ICollection<Order>> GetAllOrdersByUserId(string userId);
        Task<ICollection<Order>> GetAllOrdersByProduct(int? productId);
        Task<ICollection<Order>> GetAllOrdersByCountry(int? countryId);
        Task<Order> GetById(int? id);
        Task<bool> Create(Order model);
        Task<bool> Update(Order model);
        Task<bool> Delete(Order model);
        Task<bool> SetSend(int id);
        Task<bool> SetNotSend(int id);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
