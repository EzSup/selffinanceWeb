namespace SelfFinanceAPI.Tests.Service
{
    public class FinancialOperationServiceTests
    {
        private async Task<SelfFinanceDbContext> GetDatabseContext()
        {
            var options = new DbContextOptionsBuilder<SelfFinanceDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new SelfFinanceDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.ExpenseTypes.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.ExpenseTypes.Add(
                        new ExpenseType()
                        {
                            Name = "Salary",
                            IsIncome = true
                        });
                    databaseContext.FinancialOperations.Add(
                        new FinancialOperation()
                        {
                            TypeId = i,
                            Amount = i * 200,
                            DateTime = new DateTime(i, i, i, i, i, i)
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public void FinancialOperationService_GetAll_ReturnsFinOperations()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var finOperationService = new FinancialOperationsService(new FinancialOperationsRepository(dbContext));

            //act
            var result = finOperationService.GetAll().Result;
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<FinancialOperation>>();
            result.Equals(dbContext.FinancialOperations);
        }

        [Fact]
        public void FinancialOperationService_GetByID_ReturnsFinOperations()
        {
            //arrange
            var id = 1;
            var dbContext = GetDatabseContext().Result;
            var finOperationService = new FinancialOperationsService(new FinancialOperationsRepository(dbContext));

            //act
            var result = finOperationService.Get(id).Result;
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<FinancialOperation>();
            result.Equals(dbContext.FinancialOperations.FirstOrDefault(x => x.Id == id));
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void FinancialOperationService_Exists_ReturnsCorrectResult(int Id, bool expected)
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var finOperationService = new FinancialOperationsService(new FinancialOperationsRepository(dbContext));

            //act
            var result = finOperationService.Exists(Id).Result;
            //assert
            result.Should().Be(expected);
        }

        [Fact]
        public void FinancialOperationService_Create_ReturnsTrueWhenAdded()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var finOperationService = new FinancialOperationsService(new FinancialOperationsRepository(dbContext));
            var newFinOperationObject = new FinancialOperationForCreateDto() { TypeId = 1, Amount = 200, DateTime = new DateTime(1, 1, 1, 1, 1, 1) };
            //act
            var result = finOperationService.Create(newFinOperationObject).Result;
            //assert
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public void FinancialOperationService_Create_ThrowsException()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var finOperationService = new FinancialOperationsService(new FinancialOperationsRepository(dbContext));
            var newFinOperationObject = new FinancialOperationForCreateDto() { Amount = 200 };
            //assert
            var exception = Assert.Throws<AggregateException>(() => finOperationService.Create(newFinOperationObject).Result);
            //assert
            var innerException = exception.InnerException;
            Assert.NotNull(innerException);
            Assert.IsType<Exception>(innerException);
        }

        [Fact]
        public void FinancialOperationService_Update_ReturnsTrueWhenUpdated()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var finOperationService = new FinancialOperationsService(new FinancialOperationsRepository(dbContext));
            var newFinOperationObject = new FinancialOperationDto() { Id = 1, TypeId = 1, Amount = 276, DateTime = new DateTime(1, 2, 3, 4, 5, 6) };
            //act
            var result = finOperationService.Update(newFinOperationObject).Result;
            //assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FinancialOperationService_Update_ThrowsNullReferenceException()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var finOperationService = new FinancialOperationsService(new FinancialOperationsRepository(dbContext));
            var newFinOperationObject = new FinancialOperationDto() { Id = 0, TypeId = 1, Amount = 276, DateTime = new DateTime(1, 2, 3, 4, 5, 6) };
            //act
            var exception = Assert.Throws<AggregateException>(() => finOperationService.Update(newFinOperationObject).Result);
            //assert
            var innerException = exception.InnerException;
            Assert.NotNull(innerException);
            Assert.IsType<NullReferenceException>(innerException);
        }

        [Fact]
        public void FinancialOperationService_Delete_ReturnsTrueWhenDeleted()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var finOperationService = new FinancialOperationsService(new FinancialOperationsRepository(dbContext));
            //act
            var FinOperationObjectToDelete = finOperationService.Get(1).Result;
            var result = finOperationService.Delete(FinOperationObjectToDelete.Id).Result;
            //assert
            result.Should().BeTrue();
        }

        [Fact]
        public void FinancialOperationService_Delete_ThrowsInvalidOperationException()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var finOperationService = new FinancialOperationsService(new FinancialOperationsRepository(dbContext));
            int Id = 0;
            //act
            var exception = Assert.Throws<AggregateException>(() => finOperationService.Delete(Id).Result);
            //assert
            var innerException = exception.InnerException;
            Assert.NotNull(innerException);
            Assert.IsType<InvalidOperationException>(innerException);
        }
    }
}
