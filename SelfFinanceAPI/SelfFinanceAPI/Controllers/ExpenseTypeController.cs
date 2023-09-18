using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelfFinanceAPI.Core.Models;
using SelfFinanceAPI.Core.Services.Interfaces;
using SelfFinanceCommon.Dtos;
using SelfFinanceCommon.Dtos.ForCreate;

namespace SelfFinanceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseTypeController : Controller
    {
        private readonly IExpenseTypesService _expenseTypesService;
        private readonly IMapper _mapper;

        public ExpenseTypeController(IExpenseTypesService expenseTypesService, IMapper mapper)
        {
            _expenseTypesService = expenseTypesService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetExpenseTypes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ExpenseType>))]
        public async Task<IActionResult> GetExpenseTypes()
        {
            var expenseTypes = _mapper.Map<List<ExpenseTypeDto>>(await _expenseTypesService.GetAll());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(expenseTypes);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(ExpenseType))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetExpenseType(int Id)
        {
            var expType = await _expenseTypesService.Get(Id);
            if (expType is null)
            {
                return NotFound();
            }

            var expTypeDto = _mapper.Map<ExpenseTypeDto>(expType);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(expTypeDto);
        }

        [HttpPost]
        [Route("CreateExpenseType")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateExpenseType([FromBody] ExpenseTypeForCreateDto expTypeCreate)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if ((await _expenseTypesService.Create(expTypeCreate)) == 0)
                {
                    ModelState.AddModelError("", "Something went wrong while saving");
                    return StatusCode(500, ModelState);
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return StatusCode(422, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("UpdateExpenseType/{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateExpenseType(int Id, [FromBody] ExpenseTypeDto updatedExpType)
        {
            if (updatedExpType is null)
            {
                return BadRequest();
            }

            if (Id != updatedExpType.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            if (!(await _expenseTypesService.Update(updatedExpType)))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("DeleteExpenseType/{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteExpenseType(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (!(await _expenseTypesService.Delete(Id)))
                {
                    ModelState.AddModelError("", "Something went wrong deleting expense type. Possibly there are financial operations of this type.");
                    return BadRequest(ModelState);
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

