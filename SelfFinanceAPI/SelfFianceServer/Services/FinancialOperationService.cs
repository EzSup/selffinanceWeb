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
    public class FinancialOperationService : IFinancialOperationService
    {
        private IApiAppealService _apiAppealService;
        private ICommonService _commonService;
        private ISnackbar _snackbar;
        public FinancialOperationService(IApiAppealService apiAppealService, ICommonService commonService, ISnackbar snackbar)
        {
            _apiAppealService = apiAppealService;
            _commonService = commonService;
            _snackbar = snackbar;
        }

        public async Task<IEnumerable<FinancialOperationDto>?> GetFinancialOperationsArrayFromApi() =>
            await _apiAppealService.GetFinancialOperationsArrayFromApi();

        public async Task CreateFinancialOperation(FinancialOperationForCreateDto financialOperation)
        {
            try
            {
                var jsonContent = _commonService.Serialize(financialOperation);
                await _apiAppealService.CreateFinancialOperationApi(jsonContent);
                _snackbar.Add("The record was successfully added.", Severity.Success);
            }
            catch(Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Success);
            }
        }            

        public async Task UpdateFinancialOperation(FinancialOperationDto financialOperation)
        {
            if (financialOperation == null || financialOperation.Id < 1)
            {
                _snackbar.Add("Financial operation is empty!", Severity.Error);
                return;
            }
            try
            {
                var jsonContent = _commonService.Serialize(financialOperation);
                await _apiAppealService.UpdateFinancialOperationApi(jsonContent, financialOperation.Id);
                _snackbar.Add("The record was successfully update.", Severity.Success);
            }
            catch(Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Success);
            }
        }

        public async Task DeleteFinancialOperation(FinancialOperationDto obj)
        {
            if(!(await _commonService.CallDeletingDialog("financial operaion", obj.ToShortData())))
            {
                return;
            }

            if (obj.Id < 1)
            {
                _snackbar.Add("Id is incorrect!", Severity.Error);
                return;
            }
            try
            {
                await _apiAppealService.DeleteFinancialOperationApi(obj.Id);
                _snackbar.Add("The record was successfully deleted.", Severity.Success);
            }
            catch(Exception ex)
            {
                _snackbar.Add(ex.Message, Severity.Error);
                return;
            }
        }
    }
}
