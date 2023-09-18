using Microsoft.EntityFrameworkCore;
using SelfFinanceAPI.Core.Models;

namespace SelfFinanceAPI.Core
{
    public class SelfFinanceDbContext : DbContext
    {
        public SelfFinanceDbContext(DbContextOptions<SelfFinanceDbContext> options) : base(options) { }

        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<FinancialOperation> FinancialOperations { get; set; }
    }
}
