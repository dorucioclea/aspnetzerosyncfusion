using System.Collections.Generic;
using Practia.AppDemo.Authorization.Users.Importing.Dto;
using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
