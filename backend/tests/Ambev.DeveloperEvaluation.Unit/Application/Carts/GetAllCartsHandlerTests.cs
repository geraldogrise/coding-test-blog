using Ambev.DeveloperEvaluation.Application.Carts.GetAllCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.Common.Result;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class GetAllCartsHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly GetAllCartsHandler _handler;

        public GetAllCartsHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetAllCartsHandler(_cartRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid parameters When retrieving carts Then returns paginated carts list")]
        public async Task Handle_ValidParameters_ReturnsPaginatedCarts()
        {
            // Given
            var page = 1;
            var size = 10;
            var order = "desc";
            var carts = new List<Cart>
            {
                new Cart { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), Items = new List<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>() },
                new Cart { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), Items = new List<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>() }
            };
            var paginatedCarts = new PagedResult<Cart>(carts, carts.Count, page, size);

            _cartRepository.GetAllAsync(page, size, order, Arg.Any<CancellationToken>()).Returns(paginatedCarts);

            // When
            var command = new GetAllCartsCommand(page, size, order);
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Carts.Should().HaveCount(carts.Count);
            result.TotalItems.Should().Be(carts.Count);
            result.CurrentPage.Should().Be(page);
            result.TotalPages.Should().Be(1);
        }

        [Fact(DisplayName = "Given no carts in repository When retrieving carts Then returns empty result")]
        public async Task Handle_NoCarts_ReturnsEmptyResult()
        {
            // Given
            var page = 1;
            var size = 10;
            var order = "asc";
            var emptyPaginatedCarts = new PagedResult<Cart>(new List<Cart>(), 0, page, size);

            _cartRepository.GetAllAsync(page, size, order, Arg.Any<CancellationToken>()).Returns(emptyPaginatedCarts);

            // When
            var command = new GetAllCartsCommand(page, size, order);
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Carts.Should().BeEmpty();
            result.TotalItems.Should().Be(0);
            result.CurrentPage.Should().Be(page);
            result.TotalPages.Should().Be(0);
        }
    }
}
