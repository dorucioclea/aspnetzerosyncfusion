using System.Threading.Tasks;
using Practia.AppDemo.Sessions.Dto;

namespace Practia.AppDemo.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
