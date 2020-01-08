using System.Collections.Generic;
using Practia.AppDemo.Authorization.Users.Dto;
using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}