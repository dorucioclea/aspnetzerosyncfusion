using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
