using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Practia.AppDemo.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
