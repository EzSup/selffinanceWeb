using MudBlazor;
using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;

namespace SelfFianceServer.Services.Interfaces
{
    public interface IFinancialOperationService
    {
        Task<IEnumerable<FinancialOperationDto>?> GetFinancialOperationsArrayFromApi();

        Task CreateFinancialOperation(FinancialOperationForCreateDto expenseType);
        Task UpdateFinancialOperation(FinancialOperationDto expenseType);
        Task DeleteFinancialOperation(FinancialOperationDto obj);
    }
}
