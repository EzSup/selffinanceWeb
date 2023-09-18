namespace SelfFinanceAPI.Tests.Repository
{
    public class ExpenseTypeServiceTests
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
                        }
                );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public void ExpenseTypeService_GetAll_ReturnsExpTypes()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var expTypeService = new ExpenseTypesService(new ExpenseTypesRepository(dbContext));

            //act
            var result = expTypeService.GetAll().Result;
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<ExpenseType>>();
            result.Equals(dbContext.ExpenseTypes);
        }

        [Fact]
        public void ExpenseTypeService_GetByID_ReturnsExpType()
        {
            //arrange
            var id = 1;
            var dbContext = GetDatabseContext().Result;
            var expTypeService = new ExpenseTypesService(new ExpenseTypesRepository(dbContext));

            //act
            var result = expTypeService.Get(id).Result;
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ExpenseType>();
            result.Equals(dbContext.ExpenseTypes.FirstOrDefault(x => x.Id == id));
        }

        [Fact]
        public void ExpenseTypeService_GetByName_ReturnsExpType()
        {
            //arrange
            var name = "Salary";
            var dbContext = GetDatabseContext().Result;
            var expTypeService = new ExpenseTypesService(new ExpenseTypesRepository(dbContext));

            //act
            var result = expTypeService.Get(name).Result;
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ExpenseType>();
            result.Equals(dbContext.ExpenseTypes.FirstOrDefault(x => x.Name == name));
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void ExpenseTypeService_Exists_ReturnsCorrectResult(int Id, bool expected)
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var expTypeService = new ExpenseTypesService(new ExpenseTypesRepository(dbContext));

            //act
            var result = expTypeService.Exists(Id).Result;
            //assert
            result.Should().Be(expected);
        }

        [Fact]
        public void ExpenseTypeService_Create_ReturnsTrueWhenAdded()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var expTypeService = new ExpenseTypesService(new ExpenseTypesRepository(dbContext));
            var newExpTypeObject = new ExpenseTypeForCreateDto() { IsIncome = false, Name = "Purchase of car" };
            //act
            var result = expTypeService.Create(newExpTypeObject).Result;
            //assert
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public void ExpenseTypeService_Create_ThrowsException()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var expTypeService = new ExpenseTypesService(new ExpenseTypesRepository(dbContext));
            var newExpTypeObject = new ExpenseTypeForCreateDto() { IsIncome = true, Name = "Salary" };
            //assert
            var exception = Assert.Throws<AggregateException>(() => expTypeService.Create(newExpTypeObject).Result);
            //assert
            var innerException = exception.InnerException;
            Assert.NotNull(innerException);
            Assert.IsType<InvalidOperationException>(innerException);
        }

        [Fact]
        public void ExpenseTypeService_Update_ReturnsTrueWhenUpdated()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var expTypeService = new ExpenseTypesService(new ExpenseTypesRepository(dbContext));
            var updatedExpTypeObject = new ExpenseTypeDto() { Id = 1, IsIncome = false, Name = "Purchase of car" };
            //act
            var result = expTypeService.Update(updatedExpTypeObject).Result;
            //assert
            result.Should().BeTrue();
        }

        [Fact]
        public void ExpenseTypeService_Update_ThrowsNullReferenceException()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var expTypeService = new ExpenseTypesService(new ExpenseTypesRepository(dbContext));
            var newExpTypeObject = new ExpenseTypeDto() { IsIncome = false, Name = "123", Id = 100 };
            //assert
            var exception = Assert.Throws<AggregateException>(() => expTypeService.Update(newExpTypeObject).Result);
            //assert
            var innerException = exception.InnerException;
            Assert.NotNull(innerException);
            Assert.IsType<NullReferenceException>(innerException);
        }

        [Fact]
        public void ExpenseTypeService_Delete_ReturnsTrueWhenDeleted()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var expTypeService = new ExpenseTypesService(new ExpenseTypesRepository(dbContext));
            //act
            var ExpTypeObjectToDelete = expTypeService.Get(1).Result;
            var result = expTypeService.Delete(ExpTypeObjectToDelete.Id).Result;
            //assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void ExpenseTypeService_Delete_ThrowsInvalidOperationException()
        {
            //arrange
            var dbContext = GetDatabseContext().Result;
            var expTypeService = new ExpenseTypesService(new ExpenseTypesRepository(dbContext));
            int Id = 0;
            //assert
            var exception = Assert.Throws<AggregateException>(() => expTypeService.Delete(Id).Result);
            //assert
            var innerException = exception.InnerException;
            Assert.NotNull(innerException);
            Assert.IsType<InvalidOperationException>(innerException);
        }
    }
}
