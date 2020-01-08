using System.Threading.Tasks;
using Abp.Application.Services;
using Practia.AppDemo.Sessions.Dto;

namespace Practia.AppDemo.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
