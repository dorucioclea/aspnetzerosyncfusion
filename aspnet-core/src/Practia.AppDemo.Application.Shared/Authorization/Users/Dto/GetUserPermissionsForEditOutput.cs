using System.Collections.Generic;
using Practia.AppDemo.Authorization.Permissions.Dto;

namespace Practia.AppDemo.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}