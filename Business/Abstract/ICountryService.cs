using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Abstract
{
    public interface ICountryService
    {
        Task<ICollection<Country>> GetAll();
        ICollection<Country> GetAllSync();
        Task<ICollection<Country>> GetAllCountriesForAdd();
        Task<Country> GetById(int? id);
        Task<bool> Create(Country model);
        Task<bool> Update(Country model);
        Task<bool> Delete(Country model);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
