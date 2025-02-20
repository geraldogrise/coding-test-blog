using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
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

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductsTest
{
    public class UpdateProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly UpdateProductHandler _handler;

        public UpdateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateProductHandler(_productRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid product data When updating product Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Title = "Updated Product",
                Price = 99.99m,
                Description = "Updated Description",
                Category = "Updated Category",
                Image = "updated_image.jpg",
                Rating = new RatingDto { Rate = 4.5, Count = 100 }
            };

            var existingProduct = new Product
            {
                Id = command.Id,
                Title = "Old Product",
                Price = 49.99m,
                Description = "Old Description",
                Category = "Old Category",
                Image = "old_image.jpg",
                Rating = new RatttingDto
                {
                    Rate = 2,
                    Count = 20
                }
            };

            var updatedProduct = new Product
            {
                Id = command.Id,
                Title = command.Title,
                Price = command.Price,
                Description = command.Description,
                Category = command.Category,
                Image = command.Image,
                Rating = new RatttingDto { Rate = command.Rating.Rate, Count = command.Rating.Count }
            };

            _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns(existingProduct);
            _productRepository.UpdateAsync(command.Id, Arg.Any<Product>(), Arg.Any<CancellationToken>()).Returns(updatedProduct);
            _mapper.Map<UpdateProductResult>(updatedProduct).Returns(new UpdateProductResult { Id = updatedProduct.Id, Title = updatedProduct.Title, Price = updatedProduct.Price });

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(updatedProduct.Id);
            result.Title.Should().Be(updatedProduct.Title);
            result.Price.Should().Be(updatedProduct.Price);
            await _productRepository.Received(1).UpdateAsync(command.Id, Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given invalid product data When updating product Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new UpdateProductCommand(); // Comando vazio deve falhar na validação

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given non-existent product ID When updating Then throws key not found exception")]
        public async Task Handle_ProductNotFound_ThrowsKeyNotFoundException()
        {
            // Given
            var command = new UpdateProductCommand { Id = Guid.NewGuid(), Title = "Non-existent Product" };
            _productRepository.GetByIdAsync(command.Id, Arg.Any<CancellationToken>()).Returns((Product)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Product with ID {command.Id} not found");
        }
    }
}
