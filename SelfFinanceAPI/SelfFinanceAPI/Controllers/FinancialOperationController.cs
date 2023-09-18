using SelfFinanceCommon.Dtos.ForCreate;
using SelfFinanceCommon.Dtos;
using SelfFinanceAPI.Core.Models;
using SelfFinanceAPI.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace SelfFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialOperationController : Controller
    {
        private readonly IFinancialOperationsService _financialOperationsService;
        private readonly IMapper _mapper;

        public FinancialOperationController(IFinancialOperationsService financialOperationsService, IMapper mapper)
        {
            _financialOperationsService = financialOperationsService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetFinancialOperations")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FinancialOperationDto>))]
        public async Task<IActionResult> GetFinancialOperations()
        {
            var financialOperations = _mapper.Map<List<FinancialOperationDto>>(await _financialOperationsService.GetAll());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(financialOperations);
        }

        [HttpGet("GetFinancialOperation/{Id}")]
        [ProducesResponseType(200, Type = typeof(FinancialOperation))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetFinancialOperation(int Id)
        {
            var finOperation = await _financialOperationsService.Get(Id);

            if (finOperation is null)
            {
                return NotFound();
            }

            var finOperationDto = _mapper.Map<FinancialOperationDto>(finOperation);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(finOperationDto);
        }

        [HttpPost]
        [Route("CreateFinancialOperation")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateFinancialOperation([FromBody] FinancialOperationForCreateDto finOperationCreate)
        {
            if (finOperationCreate == null)
            {
                return BadRequest(ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _financialOperationsService.Create(finOperationCreate) == 0)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("UpdateFinancialOperation/{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateFinancialOperation(int Id, [FromBody] FinancialOperationDto updatedFinOperation)
        {
            if (updatedFinOperation is null)
            {
                return BadRequest();
            }

            if (Id != updatedFinOperation.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            if (!(await _financialOperationsService.Update(updatedFinOperation)))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DeleteFinancialOperaion/{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteFinancialOperaion(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (!(await _financialOperationsService.Delete(Id)))
                {
                    ModelState.AddModelError("", "Something went wrong deleting expense type. Possibly there are financial operations of this type.");
                }
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

