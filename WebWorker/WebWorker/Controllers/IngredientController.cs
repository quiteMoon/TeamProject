using Microsoft.AspNetCore.Mvc;
using WebWorker.BLL.Dtos.Ingredient;
using WebWorker.BLL.Services.Ingredient;

namespace WebWorker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateIngredientDto dto)
        {
            if(dto == null || string.IsNullOrEmpty(dto.Name))
                return BadRequest("Invalid ingredient data.");

            var response = await _ingredientService.CreateAsync(dto);
            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _ingredientService.DeleteAsync(id);
            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] UpdateIngredientDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid ingredient data.");

            var response = await _ingredientService.UpdateAsync(dto);

            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _ingredientService.GetByIdAsync(id);

            return response.IsSuccess ? Ok(new { 
                    message = response.Message, 
                    payload = response.Payload 
            }) : NotFound(response.Message);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _ingredientService.GetAllAsync();
            return response.IsSuccess ? Ok(new
            {
                message = response.Message,
                payload = response.Payload
            }) : BadRequest(response.Message);
        }
    }
}
