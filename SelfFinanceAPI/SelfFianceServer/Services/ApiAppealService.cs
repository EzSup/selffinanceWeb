using Microsoft.AspNetCore.Components;
using MudBlazor;
using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;
using SelfFianceServer.Services.Interfaces;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using SelfFinanceCommon;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SelfFianceServer.Services
{
    public class ApiAppealService : IApiAppealService
    {
        private readonly HttpClient _httpClient;

        public ApiAppealService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("v1");
        }
        public async Task<ReportDto?> GetReportByDayFromApi(DateTime date)
        {
            string dateString = date.ToString(Constants.DateFormat);
            var response = await _httpClient.GetAsync($"/api/Report/DailyReport?dateString={dateString}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<ReportDto>(responseStream);
            }

        }
        public async Task<ReportDto?> GetReportByPeriodOfTimeFromApi(DateTime startDate, DateTime endDate)
        {
            if(startDate > endDate)
            {
                return null;
            }
            var startDateString = startDate.ToString(Constants.DateFormat);
            var endDateString = endDate.ToString(Constants.DateFormat);
            var response = await _httpClient.GetAsync($"/api/Report/TimePeriodReport?startDateString={startDateString}&endDateString={endDateString}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<ReportDto>(responseStream);
            }

        }

        public async Task<IEnumerable<ExpenseTypeDto>?> GetExpenseTypesArrayFromApi()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/ExpenseType/GetExpenseTypes");
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<ExpenseTypeDto>>(responseStream);
            }
        }

        public async Task<IEnumerable<FinancialOperationDto>> GetFinancialOperationsArrayFromApi()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("/api/FinancialOperation/GetFinancialOperations");
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                return await JsonSerializer.DeserializeAsync<IEnumerable<FinancialOperationDto>>(responseStream);
            }
        }

        public async Task DeleteExpenseTypeApi(int Id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/Expensetype/DeleteExpenseType/{Id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("An error occurred while deleting a record.");
            }
        }

        public async Task UpdateExpenseTypeApi(StringContent jsonContent, int Id)
        {
            HttpResponseMessage response = await _httpClient.PutAsync($"/api/ExpenseType/UpdateExpenseType/{Id}", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("An error occurred while updating the record data");
            }
        }

        public async Task CreateExpenseTypeApi(StringContent jsonContent)
        {
            HttpResponseMessage response = await _httpClient.PostAsync("/api/ExpenseType/CreateExpenseType", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("An error occurred while creating a record.");
            }
        }

        public async Task DeleteFinancialOperationApi(int Id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"/api/FinancialOperation/DeleteFinancialOperaion/{Id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("An error occurred while deleting a record.");
            }
        }

        public async Task UpdateFinancialOperationApi(StringContent jsonContent, int Id)
        {
            HttpResponseMessage response = await _httpClient.PutAsync($"/api/FinancialOperation/UpdateFinancialOperation/{Id}", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("An error occurred while updating the record data");
            }
        }

        public async Task CreateFinancialOperationApi(StringContent jsonContent)
        {
            HttpResponseMessage response = await _httpClient.PostAsync("/api/FinancialOperation/CreateFinancialOperation", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("An error occurred while creating a record.");
            }
        }
    }
}
