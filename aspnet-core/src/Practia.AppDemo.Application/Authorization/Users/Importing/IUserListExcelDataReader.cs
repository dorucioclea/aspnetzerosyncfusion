using System.Collections.Generic;
using Practia.AppDemo.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Practia.AppDemo.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
