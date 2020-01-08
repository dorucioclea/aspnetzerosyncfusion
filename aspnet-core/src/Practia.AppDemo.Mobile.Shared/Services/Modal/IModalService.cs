using System.Threading.Tasks;
using Practia.AppDemo.Views;
using Xamarin.Forms;

namespace Practia.AppDemo.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}
