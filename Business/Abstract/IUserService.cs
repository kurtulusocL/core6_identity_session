using Identity_Session.Core.CrossCuttingConcern.Role.Microsoft;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Abstract
{
    public interface IUserService
    {
        Task<ICollection<ApplicationUser>> GetAll();
        Task<List<ApplicationUser>> GetAllAdmins();
        Task<List<ApplicationUser>> GetAllMembers();
        Task<ApplicationUser> GetById(string id);
        ApplicationUser GetUserByIdSync(string userId);
        Task<bool> Delete(ApplicationUser model);
        Task<bool> SetActive(string id);
        Task<bool> SetDeActive(string id);
        Task<bool> SetDeleted(string id);
        Task<bool> SetNotDeleted(string id);
    }
}
