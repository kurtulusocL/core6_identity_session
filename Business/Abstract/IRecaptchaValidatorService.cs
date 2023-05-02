namespace Identity_Session.Business.Abstract
{
    public interface IRecaptchaValidatorService
    {
        bool IsRecaptchaValid(string token);
    }
}
