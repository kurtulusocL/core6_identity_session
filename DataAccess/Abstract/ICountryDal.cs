using Identity_Session.Core.DataAccess;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.DataAccess.Abstract
{
    public interface ICountryDal : IEntityRepository<Country>
    {
        Task<List<Country>> GetAllCountries();
        List<Country> GetAllCountriesSync();
        Task<List<Country>> GetAllCountriesForAdd();
        Task<Country> GetCountryById(int? id);
        Task<bool> SetActive(int id);
        Task<bool> SetDeActive(int id);
        Task<bool> SetDeleted(int id);
        Task<bool> SetNotDeleted(int id);
    }
}
