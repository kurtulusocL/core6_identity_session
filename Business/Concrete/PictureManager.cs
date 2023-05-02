using Identity_Session.Business.Abstract;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Concrete
{
    public class PictureManager : IPictureService
    {
        readonly IPictureDal _pictureDal;
        public PictureManager(IPictureDal pictureDal)
        {
            _pictureDal = pictureDal;
        }
        public async Task<bool> Create(Picture model)
        {
            await _pictureDal.AddAsync(model);
            return true;
        }

        public bool CreateSync(Picture model)
        {
            _pictureDal.AddSync(model);
            return true;
        }

        public async Task<bool> Delete(Picture model)
        {
            await _pictureDal.DeleteAsync(model);
            return true;
        }

        public async Task<ICollection<Picture>> GetAll()
        {
            return await _pictureDal.GetAllPictures();
        }

        public async Task<ICollection<Picture>> GetAllPicturesByProductId(int? productId)
        {
            return await _pictureDal.GetAllPicturesByProductId(productId);
        }

        public async Task<ICollection<Picture>> GetAllPicturesByUserId(string userId)
        {
            return await _pictureDal.GetAllPicturesByUserId(userId);
        }

        public ICollection<Picture> GetAllSync()
        {
            return _pictureDal.GetAllPicturesSync();
        }

        public async Task<Picture> GetById(int? id)
        {
            return await _pictureDal.GetPictureById(id);
        }

        public async Task<Picture> GetPictureByProductId(int? productId)
        {
            return await _pictureDal.GetPictureByProductId(productId);
        }

        public async Task<bool> SetActive(int id)
        {
            await _pictureDal.SetActive(id);
            return true;
        }

        public async Task<bool> SetDeActive(int id)
        {
            await _pictureDal.SetDeActive(id);
            return true;
        }

        public async Task<bool> SetDeleted(int id)
        {
            await _pictureDal.SetDeleted(id);
            return true;
        }

        public async Task<bool> SetNotDeleted(int id)
        {
            await _pictureDal.SetNotDeleted(id);
            return true;
        }

        public async Task<bool> Update(Picture model)
        {
            model.UpdatedDate = DateTime.Now.ToLocalTime();
            await _pictureDal.UpdateAsync(model);
            return true;
        }
    }
}
