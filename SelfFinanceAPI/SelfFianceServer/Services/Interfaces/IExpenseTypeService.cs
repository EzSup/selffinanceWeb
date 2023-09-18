using MudBlazor;
using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;

namespace SelfFianceServer.Services.Interfaces
{
    public interface IExpenseTypeService
    {
        List<double> GetSpendsOrIncomesListFromReport(ReportDto report, ExpenseTypeDto[] expenseTypes, bool IsIncome);
        List<string> GetSpendsOrIncomesLabelsFromReport(ReportDto report, ExpenseTypeDto[] expenseTypes, bool IsIncome);

        Task<IEnumerable<ExpenseTypeDto>?> GetExpenseTypesArrayFromApi();

        Task CreateExpenseType(ExpenseTypeForCreateDto expenseType);
        Task UpdateExpenseType(ExpenseTypeDto expenseType);
        Task DeleteExpenseType(ExpenseTypeDto obj);
    }
}
