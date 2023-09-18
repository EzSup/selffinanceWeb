using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;
using SelfFianceServer.Services.Interfaces;
using MudBlazor;

namespace SelfFianceServer.Services
{
    public class ReportService : IReportService
    {
        private IApiAppealService _apiAppealService;
        private ISnackbar _snackbar;
        public ReportService(IApiAppealService apiAppealService, ISnackbar snackbar)
        {
            _apiAppealService = apiAppealService;
            _snackbar = snackbar;
        }

        public async Task<ReportDto?> GetReportByDayFromApi(DateTime date) 
        {
            var report = await _apiAppealService.GetReportByDayFromApi(date);
            if (report == null)
            {
                _snackbar.Add("An error occured while loading report.", Severity.Error);
                return null;
            }
            _snackbar.Add("Report was loaded", Severity.Info);
            return report;
        }


        public async Task<ReportDto?> GetReportByPeriodOfTimeFromApi(DateTime startDate, DateTime endDate)
        {
            var report = await _apiAppealService.GetReportByPeriodOfTimeFromApi(startDate, endDate);
            if(report == null)
            {
                _snackbar.Add("Error. Try to edit entered dates", Severity.Error);
                return null;
            }
            _snackbar.Add("Report was loaded",Severity.Info);
            return report;
        }
    }
}
