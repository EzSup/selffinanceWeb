using AutoMapper;
using SelfFinanceAPI.Controllers;
using SelfFinanceAPI.Core.Services;
using SelfFinanceAPI.Core.Services.Interfaces;

namespace SelfFinanceAPI.Tests.Controller
{
    public class FinancialOperationControllerTests
    {
        private readonly IFinancialOperationsService _financialOperationsService;
        private readonly IMapper _mapper;

        public FinancialOperationControllerTests()
        {
            _financialOperationsService = A.Fake<IFinancialOperationsService>();
            _mapper = A.Fake<IMapper>();
        }

        [Fact]
        public void FinancialOperationController_GetFinancialOperations_ReturnOk()
        {
            //arrange
            var financialOperations = A.Fake<ICollection<FinancialOperationDto>>();
            var finOperationsList = A.Fake<List<FinancialOperationDto>>();
            A.CallTo(() => _mapper.Map<List<FinancialOperationDto>>(financialOperations)).Returns(finOperationsList);
            var controller = new FinancialOperationController(_financialOperationsService, _mapper);
            //act
            var result = controller.GetFinancialOperations().Result;
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void FinancialOperationController_GetFinancialOperationById_ExpectedBehavior()
        {
            //arrange
            var financialOperations = A.Fake<ICollection<FinancialOperationDto>>();
            var finOperation = A.Fake<FinancialOperationDto>();
            int Id = finOperation.Id;
            A.CallTo(() => _financialOperationsService.Exists(Id)).Returns(true);
            A.CallTo(() => _mapper.Map<FinancialOperationDto>(financialOperations)).Returns(finOperation);
            var controller = new FinancialOperationController(_financialOperationsService, _mapper);
            //act
            var result = controller.GetFinancialOperation(Id).Result;
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void FinancialOperationController_CreateFinancialOperation_ExpectedBehavior()
        {
            var finOperation = A.Fake<FinancialOperationDto>();
            var finOperationMap = A.Fake<FinancialOperationForCreateDto>();
            var finOperationCreate = A.Fake<FinancialOperationForCreateDto>();
            A.CallTo(() => _financialOperationsService.Create(finOperationMap));
            var controller = new FinancialOperationController(_financialOperationsService, _mapper);
            //act
            var result = controller.CreateFinancialOperation(finOperationCreate);
            //assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void FinancialOperationController_UpdateFinancialOperation_ExpectedBehavior()
        {
            var finOperation = A.Fake<FinancialOperationDto>();
            var finOperationMap = A.Fake<FinancialOperationDto>();
            var finOperationUpdate = A.Fake<FinancialOperationDto>();
            int id = finOperationUpdate.Id;
            A.CallTo(() => _financialOperationsService.Update(finOperationMap)).Returns(true);
            var controller = new FinancialOperationController(_financialOperationsService, _mapper);
            //act
            var result = controller.UpdateFinancialOperation(id, finOperationUpdate);
            //assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void FinancialOperationController_DeleteFinancialOperation_ExpectedBehavior()
        {
            var finOperation = A.Fake<FinancialOperation>();
            int id = 1;
            A.CallTo(() => _financialOperationsService.Exists(id)).Returns(true);
            A.CallTo(() => _financialOperationsService.Get(id)).Returns(finOperation);
            A.CallTo(() => _financialOperationsService.Delete(id)).Returns(true);
            var controller = new FinancialOperationController(_financialOperationsService, _mapper);
            //act
            var result = controller.DeleteFinancialOperaion(id);
            //assert
            result.Should().NotBeNull();
        }
    }
}
