using System.Collections.Generic;
using Practia.AppDemo.Chat.Dto;
using Practia.AppDemo.Dto;

namespace Practia.AppDemo.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(List<ChatMessageExportDto> messages);
    }
}
