using Identity_Session.Business.Abstract;
using Identity_Session.Core.CrossCuttingConcern.Captche;
using Newtonsoft.Json;

namespace Identity_Session.Business.Concrete
{
    public class RecaptchaValidatorManager : IRecaptchaValidatorService
    {
        private const string GoogleRecaptchaAddress = "https://www.google.com/recaptcha/api/siteverify";

        public readonly IConfiguration Configuration;

        public RecaptchaValidatorManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public bool IsRecaptchaValid(string token)
        {
            using var client = new HttpClient();
            var response = client.GetStringAsync($@"{GoogleRecaptchaAddress}?secret={Configuration["Google:RecaptchaV3SecretKey"]}&response={token}").Result;
            var recaptchaResponse = JsonConvert.DeserializeObject<CaptcheResponse>(response);

            if (!recaptchaResponse.Success || recaptchaResponse.Score < Convert.ToDecimal(Configuration["Google:RecaptchaMinScore"]))
            {
                return false;
            }
            return true;
        }
    }
}
