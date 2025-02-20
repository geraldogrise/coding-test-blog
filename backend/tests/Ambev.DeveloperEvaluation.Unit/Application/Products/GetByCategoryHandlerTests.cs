using Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetByCategory;
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
using FluentAssertions;
using Ambev.DeveloperEvaluation.Common.Result;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class GetByCategoryHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly GetByCategoryHandler _handler;

        public GetByCategoryHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetByCategoryHandler(_productRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid category When retrieving products Then returns paginated result")]
        public async Task Handle_ValidCategory_ReturnsPaginatedResult()
        {
            // Given
            var category = "Bebidas";
            var page = 1;
            var size = 10;
            var order = "asc";
            var command = new GetByCategoryCommand(category, page, size, order);

            var products = new PagedResult<Product>
            {
                TotalItems = 2,
                TotalPages = 1,
                Data = new List<Product>
                {
                    new Product { Id = Guid.NewGuid(), Title = "Cerveja", Price = 5.0m, Description = "Cerveja Lager", Category = "Bebidas", Image = "image1.jpg", Rating = new RatttingDto { Rate = 4.5, Count = 100 } },
                    new Product { Id = Guid.NewGuid(), Title = "Refrigerante", Price = 3.0m, Description = "Refrigerante Cola", Category = "Bebidas", Image = "image2.jpg", Rating = new RatttingDto { Rate = 4.0, Count = 50 } }
                }
            };

            var expectedResult = new GetAllProductResult
            {
                TotalItems = 2,
                TotalPages = 1,
                Data = new List<ProductDTO>()
            };

            _productRepository.GetByCategoryAsync(category, page, size, order, Arg.Any<CancellationToken>()).Returns(products);
            _mapper.Map<List<ProductDTO>>(products.Data).Returns(expectedResult.Data);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.TotalItems.Should().Be(2);
            result.TotalPages.Should().Be(1);
            result.Data.Should().NotBeNull();
            await _productRepository.Received(1).GetByCategoryAsync(category, page, size, order, Arg.Any<CancellationToken>());
        }
    }
}
