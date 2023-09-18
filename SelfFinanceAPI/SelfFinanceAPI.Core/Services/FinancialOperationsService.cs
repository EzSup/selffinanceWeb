using Microsoft.Identity.Client;
using SelfFinanceAPI.Core.Models;
using SelfFinanceAPI.Core.Repositories;
using SelfFinanceAPI.Core.Repositories.Interfaces;
using SelfFinanceAPI.Core.Services.Interfaces;
using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;

namespace SelfFinanceAPI.Core.Services
{
    public class FinancialOperationsService : IFinancialOperationsService
    {
        private IFinancialOperationsRepository _financialOpetrationsRepository;

        public FinancialOperationsService(IFinancialOperationsRepository financialOpetrationsRepository)
        {
            _financialOpetrationsRepository = financialOpetrationsRepository;
        }

        public async Task<ICollection<FinancialOperation>> GetAll() => await _financialOpetrationsRepository.GetAll();
        public async Task<FinancialOperation?> Get(int id) => await _financialOpetrationsRepository.Get(id);
        public async Task<ICollection<FinancialOperation>> Get(DateTime date) => await _financialOpetrationsRepository.Get(date);
        public async Task<ICollection<FinancialOperation>> Get(DateTime startDate, DateTime endDate) => await _financialOpetrationsRepository.Get(startDate, endDate);

        public async Task<bool> Exists(int id) => await _financialOpetrationsRepository.Exists(id);
        public async Task<int> Create(FinancialOperationForCreateDto dto) => await _financialOpetrationsRepository.Create(dto);
        public async Task<bool> Update(FinancialOperationDto dto) => await _financialOpetrationsRepository.Update(dto);
        public async Task<bool> Delete(int Id) => await _financialOpetrationsRepository.Delete(Id);
        public async Task<bool> Save() => await _financialOpetrationsRepository.Save();
        
    }
}
