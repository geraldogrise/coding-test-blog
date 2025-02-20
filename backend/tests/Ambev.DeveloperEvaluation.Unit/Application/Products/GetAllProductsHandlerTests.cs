using Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;
using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Ambev.DeveloperEvaluation.Common.Result;
using FluentAssertions;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class GetAllProductsHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly GetAllProductsHandler _handler;

        public GetAllProductsHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetAllProductsHandler(_productRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid pagination When retrieving products Then returns paginated result")]
        public async Task Handle_ValidPagination_ReturnsPaginatedResult()
        {
            // Given
            var command = new GetAllProductsCommand(Page: 1, Size: 10, Order: "asc");
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Title = "Product 1", Price = 10.5m, Description = "Desc 1", Category = "Cat 1", Image = "image1.jpg", Rating = new RatttingDto { Rate = 4.5, Count = 100 } },
                new Product { Id = Guid.NewGuid(), Title = "Product 2", Price = 20.0m, Description = "Desc 2", Category = "Cat 2", Image = "image2.jpg", Rating = new RatttingDto { Rate = 3.5, Count = 200 } }
            };
            var paginatedProducts = new PagedResult<Product>(products, totalItems: 2, totalPages: 1);

            _productRepository.GetAllAsync(1, 10, "asc", Arg.Any<CancellationToken>()).Returns(Task.FromResult(paginatedProducts));
            _mapper.Map<List<ProductDTO>>(products).Returns(products.Select(p => new ProductDTO { Id = p.Id, Title = p.Title }).ToList());

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.TotalItems.Should().Be(2);
            result.TotalPages.Should().Be(1);
            result.Data.Should().HaveCount(2);
        }

        [Fact(DisplayName = "Given no products When retrieving products Then returns empty result")]
        public async Task Handle_NoProducts_ReturnsEmptyResult()
        {
            // Given
            var command = new GetAllProductsCommand(Page: 1, Size: 10, Order: "asc");
            var paginatedProducts = new PagedResult<Product>(new List<Product>(), totalItems: 0, totalPages: 0);

            _productRepository.GetAllAsync(1, 10, "asc", Arg.Any<CancellationToken>()).Returns(Task.FromResult(paginatedProducts));
            _mapper.Map<List<ProductDTO>>(Arg.Any<List<Product>>()).Returns(new List<ProductDTO>());

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.TotalItems.Should().Be(0);
            result.TotalPages.Should().Be(0);
            result.Data.Should().BeEmpty();
        }
    }
}
