using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfFinanceAPI.Core.Services.Interfaces
{
    public interface IReportsService
    {
        /// <summary>
        /// Makes the report of financial operations for the date
        /// </summary>
        /// <param name="date">The date</param>
        /// <returns>ReportDto</returns>
        public Task<ReportDto> DailyReport(DateTime date);

        /// <summary>
        /// Makes the report of financial operations between the dates
        /// </summary>
        /// <param name="startDate">First date</param>
        /// <param name="endDate">Last date</param>
        /// <returns>ReportDto</returns>
        public Task<ReportDto> PeriodReport(DateTime startDate, DateTime endDate);
    }
}
