using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Practia.AppDemo.DataExporting.Excel.EpPlus;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;
using Practia.AppDemo.Storage;

namespace Practia.AppDemo.Dbo.Exporting
{
    public class ResourcesExcelExporter : EpPlusExcelExporterBase, IResourcesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ResourcesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetResourceForViewDto> resources)
        {
            return CreateExcelPackage(
                "Resources.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Resources"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Code")
                        );

                    AddObjects(
                        sheet, 2, resources,
                        _ => _.Resource.Name,
                        _ => _.Resource.Code
                        );

					

                });
        }
    }
}
