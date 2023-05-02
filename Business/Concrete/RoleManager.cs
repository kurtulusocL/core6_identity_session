using Identity_Session.Business.Abstract;
using Identity_Session.Core.CrossCuttingConcern.Role.Microsoft;
using Identity_Session.DataAccess.Abstract;

namespace Identity_Session.Business.Concrete
{
    public class RoleManager : IRoleService
    {
        readonly IRoleDal _roleDal;
        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }
        public async Task<bool> Create(ApplicationRole model)
        {
            await _roleDal.AddAsync(model);
            return true;
        }

        public async Task<bool> Delete(ApplicationRole model)
        {
            await _roleDal.DeleteAsync(model);
            return true;
        }

        public async Task<ICollection<ApplicationRole>> GetAll()
        {
            var result= await _roleDal.GetAllAsync(i=>i.IsConfirmed==true&&i.IsDeleted==false);
            return result.OrderByDescending(i => i.CreatedDate).ToList();
        }

        public async Task<ApplicationRole> GetById(string id)
        {
            return await _roleDal.GetAsync(i => i.Id == id);
        }

        public async Task<bool> SetActive(string id)
        {
            await _roleDal.SetActive(id);
            return true;
        }

        public async Task<bool> SetDeActive(string id)
        {
            await _roleDal.SetDeActive(id);
            return true;
        }

        public async Task<bool> SetDeleted(string id)
        {
            await _roleDal.SetDeleted(id);
            return true;
        }

        public async Task<bool> SetNotDeleted(string id)
        {
            await _roleDal.SetNotDeleted(id);
            return true;
        }

        public async Task<bool> Update(ApplicationRole model)
        {
            model.UpdatedDate = DateTime.Now.ToLocalTime();
            await _roleDal.UpdateAsync(model);
            return true;
        }
    }
}
