using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Practia.AppDemo.DataExporting.Excel.EpPlus;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;
using Practia.AppDemo.Storage;

namespace Practia.AppDemo.Dbo.Exporting
{
    public class TestExcelExporter : EpPlusExcelExporterBase, ITestExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TestExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTestForViewDto> test)
        {
            return CreateExcelPackage(
                "Test.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Test"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Code")
                        );

                    AddObjects(
                        sheet, 2, test,
                        _ => _.Test.Name,
                        _ => _.Test.Code
                        );

					

                });
        }
    }
}
