using Identity_Session.Core.CrossCuttingConcern.Role.Microsoft;
using Identity_Session.Core.DataAccess;

namespace Identity_Session.DataAccess.Abstract
{
    public interface IRoleDal : IEntityRepository<ApplicationRole>
    {
        Task<bool> SetActive(string id);
        Task<bool> SetDeActive(string id);
        Task<bool> SetDeleted(string id);
        Task<bool> SetNotDeleted(string id);
    }
}
