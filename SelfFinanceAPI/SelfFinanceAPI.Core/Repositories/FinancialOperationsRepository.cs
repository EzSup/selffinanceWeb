using Microsoft.EntityFrameworkCore;
using SelfFinanceAPI.Core.Models;
using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;
using SelfFinanceAPI.Core.Repositories.Interfaces;

namespace SelfFinanceAPI.Core.Repositories
{
    public class FinancialOperationsRepository : IFinancialOperationsRepository
    {
        private readonly SelfFinanceDbContext _dbContext;
        public FinancialOperationsRepository(SelfFinanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<FinancialOperation>> GetAll()
        {
            return await _dbContext.FinancialOperations.OrderBy(x => x.DateTime).ToListAsync();
        }

        public async Task<FinancialOperation?> Get(int id)
        {
            return await _dbContext.FinancialOperations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<FinancialOperation>> Get(DateTime date)
        {
            return await _dbContext.FinancialOperations
                .Where(x => x.DateTime.Date == date.Date)
                .OrderBy(x => x.DateTime)
                .ToListAsync(); ;
        }

        public async Task<ICollection<FinancialOperation>> Get(DateTime startDate, DateTime endDate)
        {
            return await _dbContext.FinancialOperations
                .Where(x => x.DateTime.Date >= startDate.Date && x.DateTime.Date <= endDate.Date)
                .OrderBy(x => x.DateTime)
                .ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext.FinancialOperations.AnyAsync(x => x.Id == id);
        }

        public async Task<int> Create(FinancialOperationForCreateDto dto)
        {
            if (!CheckIfContainsExpenseType(dto.TypeId))
            {
                throw new Exception("No found expense type");
            }
            FinancialOperation finOperation = new()
            { 
                TypeId = (int)dto.TypeId, 
                Amount = (decimal)dto.Amount,
                DateTime = (DateTime)dto.DateTime, 
                Description = dto.Description 
            };
            _dbContext.Add(finOperation);
            if(await Save())
            {
                return finOperation.Id;
            }
            return 0;
        }

        public async Task<bool> Update(FinancialOperationDto dto)
        {
            FinancialOperation financialOperation = await Get(dto.Id);
            if (financialOperation is null)
            {
                throw new NullReferenceException("Not found");
            }
            financialOperation.TypeId = dto.TypeId;
            financialOperation.Amount = dto.Amount;
            financialOperation.DateTime = (DateTime)dto.DateTime;
            financialOperation.Description = dto.Description;
            return await Save();
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await Get(id);
            if (obj is null)
            {
                throw new InvalidOperationException("Object with wthis id was not found");
            }
            _dbContext.Remove(obj);
            return await Save();
        }

        public async Task<bool> Save()
        {
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }

        private bool CheckIfContainsExpenseType(int id)
        {
            return _dbContext.ExpenseTypes.Any(x => x.Id == id);
        }
    }
}
