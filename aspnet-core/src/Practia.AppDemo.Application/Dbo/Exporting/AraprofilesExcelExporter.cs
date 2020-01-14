using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Practia.AppDemo.DataExporting.Excel.EpPlus;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;
using Practia.AppDemo.Storage;

namespace Practia.AppDemo.Dbo.Exporting
{
    public class AraprofilesExcelExporter : EpPlusExcelExporterBase, IAraprofilesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public AraprofilesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetAraprofileForViewDto> araprofiles)
        {
            return CreateExcelPackage(
                "Araprofiles.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Araprofiles"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("prof_id"),
                        L("prof_description")
                        );

                    AddObjects(
                        sheet, 2, araprofiles,
                        _ => _.Araprofile.prof_id,
                        _ => _.Araprofile.prof_description
                        );

					

                });
        }
    }
}
