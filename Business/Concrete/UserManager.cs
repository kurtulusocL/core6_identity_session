using Identity_Session.Business.Abstract;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Concrete
{
    public class UserManager : IUserService
    {
        readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public async Task<bool> Delete(ApplicationUser model)
        {
            await _userDal.DeleteAsync(model);
            return true;
        }

        public async Task<ICollection<ApplicationUser>> GetAll()
        {
            return await _userDal.GetAllUsers();
        }

        public async Task<List<ApplicationUser>> GetAllAdmins()
        {
            return await _userDal.GetAllAdmins();
        }

        public async Task<List<ApplicationUser>> GetAllMembers()
        {
            return await _userDal.GetAllMembers();
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userDal.GetUserById(id);
        }

        public ApplicationUser GetUserByIdSync(string userId)
        {
            return _userDal.GetUserByIdSync(userId);
        }

        public async Task<bool> SetActive(string id)
        {
            await _userDal.SetActive(id);
            return true;
        }

        public async Task<bool> SetDeActive(string id)
        {
            await _userDal.SetDeActive(id);
            return true;
        }

        public async Task<bool> SetDeleted(string id)
        {
            await _userDal.SetDeleted(id);
            return true;
        }

        public async Task<bool> SetNotDeleted(string id)
        {
            await _userDal.SetNotDeleted(id);
            return true;
        }
    }
}
