using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Practia.AppDemo.MultiTenancy.HostDashboard.Dto;

namespace Practia.AppDemo.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}