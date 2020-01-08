using System.Threading.Tasks;

namespace Practia.AppDemo.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}