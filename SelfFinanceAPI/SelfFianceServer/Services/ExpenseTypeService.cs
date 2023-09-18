using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;
using SelfFianceServer.Services.Interfaces;
using System.Text.Json;
using System.Text;
using MudBlazor;
using MudBlazor.Extensions;
using SelfFianceServer.Pages.Dialogs;

namespace SelfFianceServer.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private IApiAppealService _apiAppealService;
        private ICommonService _commonService;
        private ISnackbar _snackbarService;
        public ExpenseTypeService(IApiAppealService apiAppealService, ICommonService commonService, ISnackbar snackbarService)
        {
            _apiAppealService = apiAppealService;
            _commonService = commonService;
            _snackbarService = snackbarService;
        }

        public async Task<IEnumerable<ExpenseTypeDto>?> GetExpenseTypesArrayFromApi() =>
            await _apiAppealService.GetExpenseTypesArrayFromApi();        

        public List<double> GetSpendsOrIncomesListFromReport(ReportDto report, ExpenseTypeDto[] expenseTypes, bool IsIncome)
        {
            return report.operations
            .Where(x => expenseTypes.Any() && expenseTypes.First(d => d.Id == x.TypeId).IsIncome == IsIncome)
            .OrderBy(x => x.TypeId)
            .GroupBy(x => x.TypeId)
            .Select(group => group.Sum(x => Convert.ToDouble(x.Amount)))
            .ToList();
        }
        public List<string> GetSpendsOrIncomesLabelsFromReport(ReportDto report, ExpenseTypeDto[] expenseTypes, bool IsIncome)
        {
            return expenseTypes
                .Where(x => x.IsIncome == IsIncome && report.operations
                .Any(d => d.TypeId == x.Id))
                .OrderBy(x => x.Id)
                .Select(x => x.Name)
                .ToList();
        }

        public async Task CreateExpenseType(ExpenseTypeForCreateDto expenseType)
        {
            try
            {
                var jsonContent = _commonService.Serialize(expenseType);
                await _apiAppealService.CreateExpenseTypeApi(jsonContent);
                _snackbarService.Add("The record was successfully added.", Severity.Success);
            }
            catch(Exception ex)
            {
                _snackbarService.Add(ex.Message, Severity.Error);
                return;
            }
        }

        public async Task UpdateExpenseType(ExpenseTypeDto expenseType) 
        {
            if(expenseType == null || expenseType.Id < 1)
            {
                _snackbarService.Add("Expense type is empty!", Severity.Error);
                return;
            }            
            try
            {
                var jsonContent = _commonService.Serialize(expenseType);
                await _apiAppealService.UpdateExpenseTypeApi(jsonContent, expenseType.Id);
                _snackbarService.Add("The record was successfully updated.", Severity.Success);
            }
            catch (Exception ex)
            {
                _snackbarService.Add(ex.Message, Severity.Error);
                return;
            }
        }

        public async Task DeleteExpenseType(ExpenseTypeDto obj)
        {
            if(!(await _commonService.CallDeletingDialog("expense type", obj.ToShortData())))
            {
                return;
            }

            if(obj.Id < 1)
            {
                _snackbarService.Add("Id is incorrect!", Severity.Error);
                return;
            }
            try
            {
                await _apiAppealService.DeleteExpenseTypeApi(obj.Id);
                _snackbarService.Add("The record was successfully deleted.", Severity.Success);
            }
            catch(Exception ex)
            {
                _snackbarService.Add(ex.Message, Severity.Error);
                return;
            }
        }
    }
}
