using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Practia.AppDemo.DataExporting.Excel.EpPlus;
using Practia.AppDemo.Dbo.Dtos;
using Practia.AppDemo.Dto;
using Practia.AppDemo.Storage;

namespace Practia.AppDemo.Dbo.Exporting
{
    public class ArausersExcelExporter : EpPlusExcelExporterBase, IArausersExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ArausersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetArauserForViewDto> arausers)
        {
            return CreateExcelPackage(
                "Arausers.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("Arausers"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("user_id"),
                        L("user_name"),
                        L("user_real_name"),
                        L("user_email"),
                        (L("Araprofile")) + L("prof_id")
                        );

                    AddObjects(
                        sheet, 2, arausers,
                        _ => _.Arauser.user_id,
                        _ => _.Arauser.user_name,
                        _ => _.Arauser.user_real_name,
                        _ => _.Arauser.user_email,
                        _ => _.Araprofileprof_id
                        );

					

                });
        }
    }
}
