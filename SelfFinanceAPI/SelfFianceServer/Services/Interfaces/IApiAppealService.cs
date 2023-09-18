using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;
using System.Text.Json;
using System.Text;

namespace SelfFianceServer.Services.Interfaces
{
    public interface IApiAppealService
    {
        Task<ReportDto?> GetReportByDayFromApi(DateTime date);
        Task<ReportDto?> GetReportByPeriodOfTimeFromApi(DateTime startDate, DateTime endDate);
        Task<IEnumerable<ExpenseTypeDto>?> GetExpenseTypesArrayFromApi();
        Task<IEnumerable<FinancialOperationDto>> GetFinancialOperationsArrayFromApi();
        Task CreateExpenseTypeApi(StringContent jsonContent);
        Task DeleteExpenseTypeApi(int Id);
        Task DeleteFinancialOperationApi(int Id);
        Task UpdateExpenseTypeApi(StringContent jsonContent, int Id);
        Task UpdateFinancialOperationApi(StringContent jsonContent, int Id);
        Task CreateFinancialOperationApi(StringContent jsonContent);
    }
}
