using System.Collections.Generic;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Dbo.Exporting
{
    public interface ITestTablesExcelExporter
    {
        FileDto ExportToFile(List<GetTestTableForViewDto> testTables);
    }
}