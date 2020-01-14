using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Practia.AppDemo.DataExporting.Excel.EpPlus;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;
using Practia.AppDemo.Storage;

namespace Practia.AppDemo.Dbo.Exporting
{
    public class TestTablesExcelExporter : EpPlusExcelExporterBase, ITestTablesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TestTablesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTestTableForViewDto> testTables)
        {
            return CreateExcelPackage(
                "TestTables.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("TestTables"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("Name"),
                        L("Code")
                        );

                    AddObjects(
                        sheet, 2, testTables,
                        _ => _.TestTable.Name,
                        _ => _.TestTable.Code
                        );

					

                });
        }
    }
}
