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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();
            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            if (!response.IsSuccess)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto dto)
        {
            var response = await _categoryService.CreateAsync(dto);
            if (!response.IsSuccess)
                return BadRequest(response);

            return StatusCode(201, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryDto dto)
        {
            if (id != dto.Id)
                return BadRequest(ServiceResponse.Error("Id mismatch"));

            var response = await _categoryService.UpdateAsync(dto);
            if (!response.IsSuccess)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _categoryService.DeleteAsync(id);
            if (!response.IsSuccess)
                return NotFound(response);

            return Ok(response);
        }
    }
}
