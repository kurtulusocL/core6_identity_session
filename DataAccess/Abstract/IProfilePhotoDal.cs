using Identity_Session.Core.DataAccess;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.DataAccess.Abstract
{
    public interface IProfilePhotoDal : IEntityRepository<ProfilePhoto>
    {
        Task<List<ProfilePhoto>> GetAllProfilePhotos();
        Task<List<ProfilePhoto>> GetAllProfilePhotoByUserId(string userId);
        Task<ProfilePhoto> GetProfilePhotoByUserId(string userId);
        Task<ProfilePhoto> GetProfilePhotoById(int? id);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
