using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetByCategory;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.HetCategories;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllPorduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByCategory;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{

    /// <summary>
    /// Controller for managing product operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of ProductsController
        /// </summary>
        /// <param name="mediator">The mediator instance</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="request">The product creation request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created product details</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Data = _mapper.Map<CreateProductResponse>(response)
            });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest(new ApiResponse { Success = false, Message = "ID in URL does not match ID in request body" });


            var validator = new UpdateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            request.Id = id;
            var command = _mapper.Map<UpdateProductCommand>(request);
           
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<UpdateProductResponse>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = _mapper.Map<UpdateProductResponse>(response)
            });
        }

        /// <summary>
        /// Retrieves a product by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the product</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The product details if found</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetProductRequest { Id = id };
            var validator = new GetProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetProductCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<GetProductResponse>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = _mapper.Map<GetProductResponse>(response)
            });
        }

        /// <summary>
        /// Retrieves all products with optional pagination and sorting
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="page">Page number (default: 1)</param>
        /// <param name="size">Number of items per page (default: 10)</param>
        /// <param name="order">Sorting order (ASC or DESC, default: ASC)</param>
        /// <returns>A paginated list of products</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<GetAllProductsResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts(
            CancellationToken cancellationToken,
            [FromQuery] int page = 1,
            [FromQuery] int size = 10,
            [FromQuery] string order = "ASC")
        {
            var query = new GetAllProductsCommand(page, size, order);
            var response = await _mediator.Send(query, cancellationToken);

            return Ok(new ApiResponseWithData<GetAllProductsResponse>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = _mapper.Map<GetAllProductsResponse>(response)
            });
        }



        /// <summary>
        /// Retrieves all product categories
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of all product categories</returns>
        [HttpGet("categories")]
        [ProducesResponseType(typeof(ApiResponseWithData<List<GetCategoriesResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetCategoriesCommand(), cancellationToken);

            return Ok(new ApiResponseWithData<GetCategoriesResponse>
            {
                Success = true,
                Message = "Categories retrieved successfully",
                Data = _mapper.Map<GetCategoriesResponse>(response)
            });
        }
        [HttpGet("category/{category}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductsByCategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByCategory(
            [FromRoute] string category,
            [FromQuery] int? page,
            [FromQuery] int? size,
            [FromQuery] string? order,
            CancellationToken cancellationToken)
        {
            // Definindo valores padrão caso sejam nulos
            int currentPage = page ?? 1;
            int pageSize = size ?? 10;
            string sortOrder = order ?? "asc";

            var request = new GetProductsByCategoryRequest
            {
                Category = category,
                Page = currentPage,
                Size = pageSize,
                Order = sortOrder
            };

            var validator = new GetProductsByCategoryRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _mediator.Send(new GetByCategoryCommand(category, currentPage, pageSize, sortOrder), cancellationToken);

            return Ok(new ApiResponseWithData<GetProductsByCategoryResponse>
            {
                Success = true,
                Message = $"Products in category '{category}' retrieved successfully",
                Data = _mapper.Map<GetProductsByCategoryResponse>(response)
            });
        }
        /// <summary>
        /// Deletes a product by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the product to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the product was deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteProductRequest { Id = id };
            var validator = new DeleteProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteProductCommand>(request.Id);
            var response = await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponseWithData<GetProductResponse>
            {
                Success = true,
                Message = "Product deleted successfully",
                Data = _mapper.Map<GetProductResponse>(response)
            });
        }
    }
}
