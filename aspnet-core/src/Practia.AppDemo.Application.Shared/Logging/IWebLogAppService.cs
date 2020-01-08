using Abp.Application.Services;
using Practia.AppDemo.Dto;
using Practia.AppDemo.Logging.Dto;

namespace Practia.AppDemo.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
