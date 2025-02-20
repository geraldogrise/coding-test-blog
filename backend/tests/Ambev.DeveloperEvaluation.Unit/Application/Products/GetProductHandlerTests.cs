using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class GetProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly GetProductHandler _handler;

        public GetProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetProductHandler(_productRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid product ID When retrieving product Then returns product result")]
        public async Task Handle_ValidProductId_ReturnsProductResult()
        {
            // Given
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Title = "Sample Product", Price = 10.5m, Description = "Test Desc", Category = "Test Category", Image = "image_url", Rating = new RatttingDto { Rate = 4.5, Count = 10 } };
            var command = new GetProductCommand(productId);
            var expectedResult = new GetProductResult { Id = product.Id, Title = product.Title };

            _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns(product);
            _mapper.Map<GetProductResult>(product).Returns(expectedResult);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(productId);
            result.Title.Should().Be("Sample Product");
            await _productRepository.Received(1).GetByIdAsync(productId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given an invalid product ID When retrieving product Then throws KeyNotFoundException")]
        public async Task Handle_InvalidProductId_ThrowsKeyNotFoundException()
        {
            // Given
            var productId = Guid.NewGuid();
            var command = new GetProductCommand(productId);

            _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns((Product)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Product with ID {productId} not found");
        }

        [Fact(DisplayName = "Given an invalid command When retrieving product Then throws ValidationException")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new GetProductCommand(Guid.Empty); // ID inválido

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
