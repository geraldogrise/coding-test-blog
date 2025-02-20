using Ambev.DeveloperEvaluation.Application.CartsItems.GetAllCartItem;
using Ambev.DeveloperEvaluation.Application.CartsItems;
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

namespace Ambev.DeveloperEvaluation.Unit.Application.CartItem
{
    public class GetAllCartItemsHandlerTests
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly GetAllCartItemsHandler _handler;

        public GetAllCartItemsHandlerTests()
        {
            _cartItemRepository = Substitute.For<ICartItemRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetAllCartItemsHandler(_cartItemRepository, _mapper);
        }

        [Fact(DisplayName = "Given valid parameters When retrieving cart items Then returns paginated cart items list")]
        public async Task Handle_ValidParameters_ReturnsPaginatedCartItems()
        {
            // Given
            var page = 1;
            var size = 10;
            var order = "desc";
            var cartItems = new List<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>
            {
                new Ambev.DeveloperEvaluation.Domain.Entities.CartItem { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 2 },
                new Ambev.DeveloperEvaluation.Domain.Entities.CartItem { Id = Guid.NewGuid(), ProductId = Guid.NewGuid(), Quantity = 5 }
            };
            var paginatedCartItems = new PagedResult<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>(cartItems, cartItems.Count, page, size);

            _cartItemRepository.GetAllAsync(page, size, order, Arg.Any<CancellationToken>()).Returns(paginatedCartItems);
            _mapper.Map<List<CartItemDto>>(cartItems).Returns(cartItems.Select(ci => new CartItemDto { Id = ci.Id, ProductId = ci.ProductId, Quantity = ci.Quantity }).ToList());

            // When
            var command = new GetAllCartItemsCommand(page, size, order);
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Data.Should().HaveCount(cartItems.Count);
            result.TotalItems.Should().Be(cartItems.Count);
            result.TotalPages.Should().Be(1);
        }

        [Fact(DisplayName = "Given no cart items in repository When retrieving cart items Then returns empty result")]
        public async Task Handle_NoCartItems_ReturnsEmptyResult()
        {
            // Given
            var page = 1;
            var size = 10;
            var order = "asc";
            var emptyPaginatedCartItems = new PagedResult<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>(new List<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>(), 0, page, size);

            _cartItemRepository.GetAllAsync(page, size, order, Arg.Any<CancellationToken>()).Returns(emptyPaginatedCartItems);

            // When
            var command = new GetAllCartItemsCommand(page, size, order);
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Data.Should().BeEmpty();
            result.TotalItems.Should().Be(0);
            result.TotalPages.Should().Be(0);
        }
    }
}
