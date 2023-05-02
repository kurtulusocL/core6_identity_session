using Identity_Session.Business.Abstract;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Concrete
{
    public class ProfilePhotoManager : IProfilePhotoService
    {
        readonly IProfilePhotoDal _profilePhotoDal;
        public ProfilePhotoManager(IProfilePhotoDal profilePhotoDal)
        {
            _profilePhotoDal = profilePhotoDal;
        }
        public async Task<bool> Create(ProfilePhoto model)
        {
            await _profilePhotoDal.AddAsync(model);
            return true;
        }

        public async Task<bool> Delete(ProfilePhoto model)
        {
            await _profilePhotoDal.DeleteAsync(model);
            return true;
        }

        public async Task<ICollection<ProfilePhoto>> GetAll()
        {
            return await _profilePhotoDal.GetAllProfilePhotos();
        }

        public async Task<ICollection<ProfilePhoto>> GetAllProfilePhotoByUserId(string userId)
        {
            return await _profilePhotoDal.GetAllProfilePhotoByUserId(userId);
        }

        public async Task<ProfilePhoto> GetById(int? id)
        {
            return await _profilePhotoDal.GetProfilePhotoById(id);
        }

        public async Task<ProfilePhoto> GetProfilePhotoByUserId(string userId)
        {
            return await _profilePhotoDal.GetProfilePhotoByUserId(userId);
        }

        public async Task<bool> SetActive(int id)
        {
            await _profilePhotoDal.SetActive(id);
            return true;
        }

        public async Task<bool> SetDeActive(int id)
        {
            await _profilePhotoDal.SetDeActive(id);
            return true;
        }

        public async Task<bool> SetDeleted(int id)
        {
            await _profilePhotoDal.SetDeleted(id);
            return true;
        }

        public async Task<bool> SetNotDeleted(int id)
        {
            await _profilePhotoDal.SetNotDeleted(id);
            return true;
        }

        public async Task<bool> Update(ProfilePhoto model)
        {
            model.UpdatedDate = DateTime.Now.ToLocalTime();
            await _profilePhotoDal.UpdateAsync(model);
            return true;
        }
    }
}
