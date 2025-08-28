using Microsoft.AspNetCore.Mvc;
using WebWorker.BLL.Dtos.Category;
using WebWorker.BLL.Services;
using WebWorker.BLL.Services.Category;

namespace WebWorker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            if (!response.IsSuccess)
                return BadRequest(response.Message);

            return Ok(new {
                message = response.Message,
                payload = response.Payload
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            if (!response.IsSuccess)
                return NotFound(response.Message);

            return Ok(new {
                message = response.Message,
                payload = response.Payload
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Name))
                return BadRequest("Invalid category data.");

            var response = await _categoryService.CreateAsync(dto);
            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] UpdateCategoryDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid category data.");

            var response = await _categoryService.UpdateAsync(dto);
            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }
    }
}
