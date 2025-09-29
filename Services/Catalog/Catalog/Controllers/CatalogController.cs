using Catalog.Commands;
using Catalog.DTOs;
using Catalog.Extensions;
using Catalog.Queries;
using Catalog.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<IList<ProductDto>>> GetAllProducts([FromQuery] CatalogSpecParams catalogSpecParams)
        {
            var query = new GetAllProductQuery(catalogSpecParams);
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string id) {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

        [HttpGet("productName/{productName}")]
        public async Task<ActionResult<IList<ProductDto>>> GetProductByName(string productName)
        {
            var query = new GetProductByNameQuery(productName);
            var product = await _mediator.Send(query);
            if(product == null || !product.Any())
            {
                return NotFound();
            }

            var dtoList = product.Select(p => p.ToDto()).ToList();
            return Ok(dtoList);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] CreateProductCommand productCommand)
        {
            var product = await _mediator.Send(productCommand);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var command = new DeleteProductByIdCommand(id);
            var product = await _mediator.Send(command);
            if (!product)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, UpdateProductDto updateProductDto)
        {
            var query = updateProductDto.ToCommand(id);
            var product = await _mediator.Send(query);
            if (!product)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("GetAllBrands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
        {
            var query = new GetAllBrandsQuery();
            var brands = await _mediator.Send(query);
            return Ok(brands);
        }

        [HttpGet("GetAllTypes")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetTypes()
        {
            var query = new GetAllTypesQuery();
            var types = await _mediator.Send(query);
            return Ok(types);
        }

        [HttpGet("brand/{brand}", Name = "GetProductByBrandName")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByBrand(string brand) {
            var query = new GetProductByBrandQuery(brand);
            var product = await _mediator.Send(query);
            return Ok(product);
        }

    }
}
