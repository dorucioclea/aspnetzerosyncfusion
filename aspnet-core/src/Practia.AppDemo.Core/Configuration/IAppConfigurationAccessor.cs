using Microsoft.Extensions.Configuration;

namespace Practia.AppDemo.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
