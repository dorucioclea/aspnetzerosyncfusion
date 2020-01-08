using System.Collections.Generic;
using Practia.AppDemo.Auditing.Dto;
using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
