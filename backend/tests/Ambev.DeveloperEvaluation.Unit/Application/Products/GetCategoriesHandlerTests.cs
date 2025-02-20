using Ambev.DeveloperEvaluation.Application.Products.HetCategories;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products
{
    public class GetCategoriesHandlerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly GetCategoriesHandler _handler;

        public GetCategoriesHandlerTests()
        {
            _productRepository = Substitute.For<IProductRepository>();
            _handler = new GetCategoriesHandler(_productRepository);
        }

        [Fact(DisplayName = "Given a request for categories When handling request Then returns list of categories")]
        public async Task Handle_ValidRequest_ReturnsCategoriesList()
        {
            // Given
            var expectedCategories = new List<string> { "Bebidas", "Alimentos", "Higiene" };
            _productRepository.GetCategoriesAsync(Arg.Any<CancellationToken>()).Returns(expectedCategories);
            var command = new GetCategoriesCommand();

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
            result.Should().BeEquivalentTo(expectedCategories);
        }

        [Fact(DisplayName = "Given no categories exist When handling request Then returns empty list")]
        public async Task Handle_NoCategoriesExist_ReturnsEmptyList()
        {
            // Given
            _productRepository.GetCategoriesAsync(Arg.Any<CancellationToken>()).Returns(new List<string>());
            var command = new GetCategoriesCommand();

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
