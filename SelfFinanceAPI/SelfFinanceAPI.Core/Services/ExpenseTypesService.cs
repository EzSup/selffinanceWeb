using SelfFinanceAPI.Core.Models;
using SelfFinanceAPI.Core.Repositories;
using SelfFinanceAPI.Core.Repositories.Interfaces;
using SelfFinanceAPI.Core.Services.Interfaces;
using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;

namespace SelfFinanceAPI.Core.Services
{
    public class ExpenseTypesService : IExpenseTypesService
    {
        private readonly IExpenseTypesRepository _expenseTypesRepository;

        public ExpenseTypesService(IExpenseTypesRepository expenseTypesRepository)
        {
            _expenseTypesRepository = expenseTypesRepository;
        }

        public async Task<ICollection<ExpenseType>> GetAll() => await _expenseTypesRepository.GetAll();
        public async Task<ExpenseType> Get(int id) => await _expenseTypesRepository.Get(id);
        public async Task<ExpenseType> Get(string name) => await _expenseTypesRepository.Get(name);

        public async Task<bool> Exists(int id) => await _expenseTypesRepository.Exists(id);
        public async Task<int> Create(ExpenseTypeForCreateDto dto) => await _expenseTypesRepository.Create(dto);

        public async Task<bool> Update(ExpenseTypeDto dto) => await _expenseTypesRepository.Update(dto);
        public async Task<bool> Delete(int Id) => await _expenseTypesRepository.Delete(Id);
        public async Task<bool> Save() => await _expenseTypesRepository.Save();
    }
}
