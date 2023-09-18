using Microsoft.EntityFrameworkCore;
using SelfFinanceAPI.Core.Models;
using SelfFinanceAPI.Core.Repositories.Interfaces;
using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;

namespace SelfFinanceAPI.Core.Repositories
{
    public class ExpenseTypesRepository : IExpenseTypesRepository
    {
        private readonly SelfFinanceDbContext _dbContext;

        public ExpenseTypesRepository(SelfFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<ExpenseType>> GetAll()
        {
            return await _dbContext.ExpenseTypes.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<ExpenseType?> Get(int id)
        {
            return await _dbContext.ExpenseTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ExpenseType?> Get(string name)
        {
            return await _dbContext.ExpenseTypes.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.ExpenseTypes.AnyAsync(x => x.Id == id);
        }

        public async Task<int> Create(ExpenseTypeForCreateDto dto)
        {
            if (ContainsDuplicate(dto))
            {
                throw new InvalidOperationException("Same expense  type already exists");
            }

            ExpenseType expType = new()
            {
                IsIncome = (bool)dto.IsIncome,
                Name = dto.Name
            };
            _dbContext.ExpenseTypes.Add(expType);

            if (await Save())
            {
                return expType.Id;
            }
            return 0;
        }

        public async Task<bool> Update(ExpenseTypeDto dto)
        {
            ExpenseType expType = await Get(dto.Id);
            if (expType is null)
            {
                throw new NullReferenceException("Not found");
            }
            if (!ContainsDuplicate(dto))
            {
                expType.IsIncome = dto.IsIncome;
                expType.Name = dto.Name;
            }
            return await Save();
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await Get(id);
            if (obj is null)
            {
                throw new InvalidOperationException("Object with this id was not found");
            }
            if (_dbContext.FinancialOperations.Any(x => x.TypeId == id))
            {
                return false;
            }
            _dbContext.Remove(obj);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        private bool ContainsDuplicate(ExpenseTypeForCreateDto dto)
        {
            return _dbContext.ExpenseTypes.Any(x => x.Name == dto.Name && x.IsIncome == dto.IsIncome);
        }
        private bool ContainsDuplicate(ExpenseTypeDto dto)
        {
            return _dbContext.ExpenseTypes.Any(x => x.Name == dto.Name && x.IsIncome == dto.IsIncome);
        }
    }
}

