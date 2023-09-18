using AutoMapper;
using SelfFinanceAPI.Controllers;
using SelfFinanceAPI.Core.Services.Interfaces;

namespace SelfFinanceAPI.Tests.Controller
{
    public class ExpenseTypeControllerTests
    {
        private readonly IExpenseTypesService _expenseTypesService;
        private readonly IMapper _mapper;

        public ExpenseTypeControllerTests()
        {
            _expenseTypesService = A.Fake<IExpenseTypesService>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void ExpenseTypeController_GetExpenseTypes_ReturnOk()
        {
            //arrange
            var expTypes = A.Fake<ICollection<ExpenseTypeDto>>();
            var expTypesList = A.Fake<List<ExpenseTypeDto>>();
            A.CallTo(() => _mapper.Map<List<ExpenseTypeDto>>(expTypes)).Returns(expTypesList);
            var controller = new ExpenseTypeController(_expenseTypesService, _mapper);
            //act
            var result = controller.GetExpenseTypes().Result;
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ExpenseTypeController_GetExpenseTypeById_ExpectedBehavior()
        {
            // Arrange

            var expTypes = A.Fake<ICollection<ExpenseTypeDto>>();
            var expType = A.Fake<ExpenseTypeDto>();
            int Id = expType.Id;
            A.CallTo(() => _expenseTypesService.Exists(Id)).Returns(true);
            A.CallTo(() => _mapper.Map<ExpenseTypeDto>(expTypes)).Returns(expType);
            var controller = new ExpenseTypeController(_expenseTypesService, _mapper);
            //act
            var result = controller.GetExpenseType(Id).Result;
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ExpenseTypeController_CreateExpensetype_ExpectedBehavior()
        {
            var expenseType = A.Fake<ExpenseType>();
            var expenseTypeMap = A.Fake<ExpenseTypeForCreateDto>();
            var expenseTypeCreate = A.Fake<ExpenseTypeForCreateDto>();
            A.CallTo(() => _expenseTypesService.Get(expenseTypeCreate.Name)).Returns(expenseType);
            A.CallTo(() => _expenseTypesService.Create(expenseTypeMap));
            var controller = new ExpenseTypeController(_expenseTypesService, _mapper);
            //act
            var result = controller.CreateExpenseType(expenseTypeCreate).Result;
            //assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void ExpenseTypeController_UpdateExpensetype_ExpectedBehavior()
        {
            var expenseType = A.Fake<ExpenseType>();
            var expenseTypeMap = A.Fake<ExpenseTypeDto>();
            var expenseTypeUpdate = A.Fake<ExpenseTypeDto>();
            int id = expenseTypeUpdate.Id;
            A.CallTo(() => _expenseTypesService.Update(expenseTypeMap)).Returns(true);
            var controller = new ExpenseTypeController(_expenseTypesService, _mapper);
            //act
            var result = controller.UpdateExpenseType(id, expenseTypeUpdate);
            //assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void ExpenseTypeController_DeleteExpensetype_ExpectedBehavior()
        {
            var expenseType = A.Fake<ExpenseType>();
            int id = 1;
            A.CallTo(() => _expenseTypesService.Exists(id)).Returns(true);
            A.CallTo(() => _expenseTypesService.Get(id)).Returns(expenseType);
            A.CallTo(() => _expenseTypesService.Delete(id)).Returns(true);
            var controller = new ExpenseTypeController(_expenseTypesService, _mapper);
            //act
            var result = controller.DeleteExpenseType(id);
            //assert
            result.Should().NotBeNull();
        }
    }
}
