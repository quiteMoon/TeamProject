using Microsoft.AspNetCore.Mvc;
using WebWorker.BLL.Dtos.Product;
using WebWorker.BLL.Services.Product;

namespace WebWorker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateProductDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Name) || dto.Price <= 0)
                return BadRequest("Invalid product data.");

            var responce = await _productService.CreateAsync(dto);
            return responce.IsSuccess ? Ok(responce.Message) : BadRequest(responce.Message);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] UpdateProductDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid product data.");

            var response = await _productService.UpdateAsync(dto);
            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productService.DeleteAsync(id);
            return response.IsSuccess ? Ok(response.Message) : BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _productService.GetByIdAsync(id);
            return response.IsSuccess ? Ok(new
            {
                message = response.Message,
                payload = response.Payload
            }) : NotFound(response.Message);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll(string? categoryName)
        {
            var response = await _productService.GetAllAsync(categoryName);
            return response.IsSuccess ? Ok(new
            {
                message = response.Message,
                payload = response.Payload
            }) : BadRequest(response.Message);
        }
    }
}
