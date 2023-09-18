using System.Text.Json;
using System.Text;
using SelfFianceServer.Services.Interfaces;
using MudBlazor;
using SelfFianceServer.Pages.Dialogs;

namespace SelfFianceServer.Services
{
    public class CommonService : ICommonService
    {

        private IDialogService _dialogService;

        public CommonService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public StringContent Serialize<T>(T obj) where T : class
        {
            return new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, "application/json");
        }

        public async Task<bool> CallDeletingDialog(string objectType, string objectString)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true };
            var parameters = new DialogParameters<DeletingDialog>();
            parameters.Add(x => x._type, objectType);
            parameters.Add(x => x._objectToString, objectString);
            var dialogResult = await _dialogService.Show<DeletingDialog>("Financial operation record deleting dialog", parameters, options).Result;
            return !dialogResult.Canceled;
        }
    }
}
