using Identity_Session.Business.Abstract;
using Identity_Session.DataAccess.Abstract;
using Identity_Session.Entities.Concrete;

namespace Identity_Session.Business.Concrete
{
    public class CountryManager : ICountryService
    {
        readonly ICountryDal _countryDal;
        public CountryManager(ICountryDal countryDal)
        {
            _countryDal = countryDal;
        }
        public async Task<bool> Create(Country model)
        {
            await _countryDal.AddAsync(model);
            return true;
        }

        public async Task<bool> Delete(Country model)
        {
            await _countryDal.DeleteAsync(model);
            return true;
        }

        public async Task<ICollection<Country>> GetAll()
        {
            return await _countryDal.GetAllCountries();
        }

        public async Task<ICollection<Country>> GetAllCountriesForAdd()
        {
            return await _countryDal.GetAllCountriesForAdd();
        }

        public ICollection<Country> GetAllSync()
        {
            return _countryDal.GetAllCountriesSync();
        }

        public async Task<Country> GetById(int? id)
        {
            return await _countryDal.GetCountryById(id);
        }

        public async Task<bool> SetActive(int id)
        {
            await _countryDal.SetActive(id);
            return true;
        }

        public async Task<bool> SetDeActive(int id)
        {
            await _countryDal.SetDeActive(id);
            return true;
        }

        public async Task<bool> SetDeleted(int id)
        {
            await _countryDal.SetDeleted(id);
            return true;
        }

        public async Task<bool> SetNotDeleted(int id)
        {
            await _countryDal.SetNotDeleted(id);
            return true;
        }

        public async Task<bool> Update(Country model)
        {
            model.UpdatedDate = DateTime.Now.ToLocalTime();
            await _countryDal.UpdateAsync(model);
            return true;
        }
    }
}
