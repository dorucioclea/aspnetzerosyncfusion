using System.Collections.Generic;
using MvvmHelpers;
using Practia.AppDemo.Models.NavigationMenu;

namespace Practia.AppDemo.Services.Navigation
{
    public interface IMenuProvider
    {
        ObservableRangeCollection<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}