using Abp.Authorization;
using Practia.AppDemo.Authorization.Roles;
using Practia.AppDemo.Authorization.Users;

namespace Practia.AppDemo.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
