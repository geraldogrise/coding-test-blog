using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.ProductsTest
{
    /// <summary>
    /// Contains unit tests for the <see cref="CreateProductHandler"/> class.
    /// </summary>
    public class CreateProductHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly CreateProductHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateProductHandlerTests"/> class.
        /// </summary>
        public CreateProductHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateProductHandler(_productRepository, _mapper);
        }

        /// <summary>
        /// Tests that a valid product creation request is handled successfully.
        /// </summary>
        [Fact(DisplayName = "Given valid product data When creating product Then returns success response")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Given
            var command = new CreateProductCommand
            {
                Title = "Product 1",
                Price = 50.0m,
                Description = "A great product",
                Category = "Electronics",
                Image = "image-url",
                Rating = new Ambev.DeveloperEvaluation.Application.Products.CreateProduct.RatingDto
                {
                    Rate = 4.5d,
                    Count = 100
                }
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Price = command.Price,
                Description = command.Description,
                Category = command.Category,
                Image = command.Image,
                Rating = new RatttingDto
                {
                    Rate = command.Rating.Rate,
                    Count = command.Rating.Count
                }
            };

            var expectedResult = new CreateProductResult
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                Category = product.Category,
                Image = product.Image,
                Rating = { Rate = product.Rating.Rate, Count = product.Rating.Count }
            };

            _mapper.Map<Product>(command).Returns(product);
            _mapper.Map<CreateProductResult>(product).Returns(expectedResult);
            _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
                .Returns(product);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(product.Id);
            result.Title.Should().Be(product.Title);
            result.Price.Should().Be(product.Price);
            result.Description.Should().Be(product.Description);
            result.Category.Should().Be(product.Category);
            result.Image.Should().Be(product.Image);
            result.Rating.Should().BeEquivalentTo(product.Rating);

            await _productRepository.Received(1).CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that an invalid product creation request throws a validation exception.
        /// </summary>
        [Fact(DisplayName = "Given invalid product data When creating product Then throws validation exception")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var command = new CreateProductCommand(); // Empty command will fail validation

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        /// <summary>
        /// Tests that the mapper is called with the correct command.
        /// </summary>
        [Fact(DisplayName = "Given valid command When handling Then maps command to product entity")]
        public async Task Handle_ValidRequest_MapsCommandToProduct()
        {
            // Given
            var command = new CreateProductCommand
            {
                Title = "Product X",
                Price = 120.99m,
                Description = "Another great product",
                Category = "Home Appliances",
                Image = "image-url",
                Rating = new Ambev.DeveloperEvaluation.Application.Products.CreateProduct.RatingDto
                {
                    Rate = 4.5d,
                    Count = 100
                }
            };

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Price = command.Price,
                Description = command.Description,
                Category = command.Category,
                Image = command.Image,
                Rating = new RatttingDto
                {
                    Rate = command.Rating.Rate,
                    Count = command.Rating.Count
                }
            };

            _mapper.Map<Product>(command).Returns(product);
            _productRepository.CreateAsync(Arg.Any<Product>(), Arg.Any<CancellationToken>())
                .Returns(product);

            // When
            await _handler.Handle(command, CancellationToken.None);

            // Then
            _mapper.Received(1).Map<Product>(Arg.Is<CreateProductCommand>(c =>
                c.Title == command.Title &&
                c.Price == command.Price &&
                c.Description == command.Description &&
                c.Category == command.Category &&
                c.Image == command.Image &&
                c.Rating.Rate == command.Rating.Rate &&
                c.Rating.Count == command.Rating.Count));
        }
    }
}
