using Identity_Session.Core.DataAccess;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.DataAccess.Abstract
{
    public interface IPictureDal : IEntityRepository<Picture>
    {
        Task<List<Picture>> GetAllPictures();
        List<Picture> GetAllPicturesSync();
        Task<List<Picture>> GetAllPicturesByUserId(string userId);
        Task<List<Picture>> GetAllPicturesByProductId(int? productId);
        Task<Picture> GetPictureByProductId(int? productId);
        Task<Picture> GetPictureById(int? id);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
