using Identity_Session.Core.DataAccess;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<ApplicationUser>
    {
        Task<List<ApplicationUser>> GetAllUsers();
        Task<List<ApplicationUser>> GetAllAdmins();
        Task<List<ApplicationUser>> GetAllMembers();
        Task<ApplicationUser> GetUserById(string id);
        ApplicationUser GetUserByIdSync(string userId);
        Task<bool> SetActive(string id);
        Task<bool> SetDeActive(string id);
        Task<bool> SetDeleted(string id);
        Task<bool> SetNotDeleted(string id);
    }
}
