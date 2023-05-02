using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Abstract
{
    public interface IProfilePhotoService
    {
        Task<ICollection<ProfilePhoto>> GetAll();
        Task<ICollection<ProfilePhoto>> GetAllProfilePhotoByUserId(string userId);
        Task<ProfilePhoto> GetProfilePhotoByUserId(string userId);
        Task<ProfilePhoto> GetById(int? id);
        Task<bool> Create(ProfilePhoto model);
        Task<bool> Update(ProfilePhoto model);
        Task<bool> Delete(ProfilePhoto model);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
