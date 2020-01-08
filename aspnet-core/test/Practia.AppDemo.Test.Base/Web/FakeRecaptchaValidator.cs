using System.Threading.Tasks;
using Practia.AppDemo.Security.Recaptcha;

namespace Practia.AppDemo.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
