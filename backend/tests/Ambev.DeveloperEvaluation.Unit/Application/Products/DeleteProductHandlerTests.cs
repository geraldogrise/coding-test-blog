using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class DeleteProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new DeleteProductHandler(_mapper, _productRepository);
        }

        [Fact(DisplayName = "Given a valid product ID When deleting product Then returns success response")]
        public async Task Handle_ValidProductId_ReturnsSuccessResponse()
        {
            // Given
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
                Title = "Deleted Title",
                Price = 20,
                Description = "Deleted Description",
                Category = "Deleted Category",
                Image = "Deleted image",
                Rating = new RatttingDto
                {
                    Rate = 10,
                    Count = 20,
                }
            };
            var command = new DeleteProductCommand(productId);
            var expectedResult = new GetProductResult { Id = product.Id, Title = product.Title };

            _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns(product);
            _productRepository.DeleteAsync(productId, Arg.Any<CancellationToken>()).Returns(true);
            _mapper.Map<GetProductResult>(product).Returns(expectedResult);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(productId);
            result.Title.Should().Be("Sample Product");
            await _productRepository.Received(1).DeleteAsync(productId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given an invalid product ID When deleting product Then throws KeyNotFoundException")]
        public async Task Handle_InvalidProductId_ThrowsKeyNotFoundException()
        {
            // Given
            var productId = Guid.NewGuid();
            var command = new DeleteProductCommand(productId);

            _productRepository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns((Product)null);
            _productRepository.DeleteAsync(productId, Arg.Any<CancellationToken>()).Returns(false);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Product with ID {productId} not found");
        }

        [Fact(DisplayName = "Given an invalid command When deleting product Then throws ValidationException")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new DeleteProductCommand(Guid.Empty); // ID inválido

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
