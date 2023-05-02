using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Abstract
{
    public interface IPictureService
    {
        Task<ICollection<Picture>> GetAll();
        ICollection<Picture> GetAllSync();
        Task<ICollection<Picture>> GetAllPicturesByUserId(string userId);
        Task<ICollection<Picture>> GetAllPicturesByProductId(int? productId);
        Task<Picture> GetPictureByProductId(int? productId);
        Task<Picture> GetById(int? id);
        Task<bool> Create(Picture model);
        bool CreateSync(Picture model);
        Task<bool> Update(Picture model);
        Task<bool> Delete(Picture model);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
