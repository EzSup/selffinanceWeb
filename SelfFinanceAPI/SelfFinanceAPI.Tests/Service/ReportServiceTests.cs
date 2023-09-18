using AutoMapper;
using SelfFinanceAPI.Core.Models;
using SelfFinanceAPI.Helper;

namespace SelfFinanceAPI.Tests.Service
{
    public class ReportServiceTests
    {
        private IMapper CreateMapper() 
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfiles>();
            });
            return configuration.CreateMapper();
        } 

        private async Task<SelfFinanceDbContext> GetDatabseContext()
        {
            var options = new DbContextOptionsBuilder<SelfFinanceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new SelfFinanceDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (databaseContext.ExpenseTypes.CountAsync().Result <= 0)
            {
                List<ExpenseType> expenseTypes = new()
                    {
                        new ExpenseType()
                            {
                                Name = "Salary",
                                IsIncome = true
                            },
                        new ExpenseType()
                            {
                                Name = "Dinner",
                                IsIncome = false
                            }
                    };
                databaseContext.ExpenseTypes.AddRange(expenseTypes);
                List<FinancialOperation> finOperations = new()
                    {
                        new FinancialOperation()
                            {
                                TypeId = 1,
                                Amount = 1000,
                                DateTime = new DateTime(1,1,1,1,1,1)
                            },
                        new FinancialOperation()
                            {
                                TypeId = 2,
                                Amount = 200,
                                DateTime = new DateTime(2,2,2,2,2,2)
                            },
                        new FinancialOperation()
                            {
                                TypeId = 1,
                                Amount = 300,
                                DateTime = new DateTime(3, 3, 3,3,3,3)
                            },
                        new FinancialOperation()
                            {
                                TypeId = 2,
                                Amount = 500,
                                DateTime = new DateTime(4, 4, 4,4,4,4)
                            },
                    };
                databaseContext.FinancialOperations.AddRange(finOperations);
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }


        [Fact]
        public async Task ReportsService_DailyReport_ReturnsReportDTO()
        {
            IMapper mapper = CreateMapper();
            //arrange
            var dbContext = await GetDatabseContext();
            var reportsService = new ReportsService(new FinancialOperationsRepository(dbContext),
                new ExpenseTypesRepository(dbContext), mapper);
            DateTime date = new DateTime(1,1,1);
            decimal incomeExpected = 1000;
            decimal expenseExpencted = 0;
            //act
            var result = await reportsService.DailyReport(date);
            //assert
            dbContext.Should().NotBeNull();
            result.Should().NotBeNull();
            result.Should().BeOfType<ReportDto>();
            result.totalIncome.Should().Be(incomeExpected);
            result.totalExpenses.Should().Be(expenseExpencted);
        }

        [Fact]
        public async Task ReportsService_PeriodReport_ReturnsReportDTOs()
        {
            //arrange
            IMapper mapper = CreateMapper();
            var dbContext = await GetDatabseContext();
            var reportsService = new ReportsService(new FinancialOperationsRepository(dbContext),
                new ExpenseTypesRepository(dbContext), mapper);
            DateTime startDate = new DateTime(1, 1, 1);
            DateTime endDate = new DateTime(3, 3, 3);
            decimal incomeExpected = 1300;
            decimal expenseExpencted = 200;
            //act
            var result = reportsService.PeriodReport(startDate, endDate).Result;
            //assert
            (await dbContext.FinancialOperations.CountAsync()).Should().Be(4);
            result.Should().NotBeNull();
            result.Should().BeOfType<ReportDto>();
            result.totalIncome.Should().Be(incomeExpected);
            result.totalExpenses.Should().Be(expenseExpencted);
        }

    }
}
