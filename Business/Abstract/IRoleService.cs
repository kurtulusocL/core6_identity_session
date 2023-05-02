using Identity_Session.Core.CrossCuttingConcern.Role.Microsoft;

namespace Identity_Session.Business.Abstract
{
    public interface IRoleService
    {
        Task<ICollection<ApplicationRole>> GetAll();
        Task<ApplicationRole> GetById(string id);
        Task<bool> Create(ApplicationRole model);
        Task<bool> Update(ApplicationRole model);
        Task<bool> Delete(ApplicationRole model);
        Task<bool> SetActive(string id);
        Task<bool> SetDeActive(string id);
        Task<bool> SetDeleted(string id);
        Task<bool> SetNotDeleted(string id);
    }
}
