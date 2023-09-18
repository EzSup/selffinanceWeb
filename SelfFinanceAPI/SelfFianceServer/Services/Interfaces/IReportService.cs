using MudBlazor;
using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;

namespace SelfFianceServer.Services.Interfaces
{
    public interface IReportService
    {
        Task<ReportDto?> GetReportByDayFromApi(DateTime date);
        Task<ReportDto?> GetReportByPeriodOfTimeFromApi(DateTime startDate, DateTime endDate);
    }
}
