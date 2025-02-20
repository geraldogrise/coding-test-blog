using Ambev.DeveloperEvaluation.Application.CartsItems.GetCartItem;
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

namespace Ambev.DeveloperEvaluation.Unit.Application.CartItem
{
    public class GetCartItemHandlerTests
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly GetCartItemHandler _handler;

        public GetCartItemHandlerTests()
        {
            _cartItemRepository = Substitute.For<ICartItemRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetCartItemHandler(_cartItemRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid cart item ID When retrieving cart item Then returns cart item details")]
        public async Task Handle_ValidCartItemId_ReturnsCartItemDetails()
        {
            // Given
            var cartItemId = Guid.NewGuid();
            var cartItem = new Ambev.DeveloperEvaluation.Domain.Entities.CartItem { Id = cartItemId };
            var expectedResult = new GetCartItemResult { Id = cartItem.Id };

            _cartItemRepository.GetByIdAsync(cartItemId, Arg.Any<CancellationToken>()).Returns(cartItem);
            _mapper.Map<GetCartItemResult>(cartItem).Returns(expectedResult);

            var command = new GetCartItemCommand(cartItemId);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(cartItemId);
        }

        [Fact(DisplayName = "Given an invalid cart item ID When retrieving cart item Then throws KeyNotFoundException")]
        public async Task Handle_InvalidCartItemId_ThrowsKeyNotFoundException()
        {
            // Given
            var cartItemId = Guid.NewGuid();
            var command = new GetCartItemCommand(cartItemId);

            _cartItemRepository.GetByIdAsync(cartItemId, Arg.Any<CancellationToken>()).Returns((Ambev.DeveloperEvaluation.Domain.Entities.CartItem)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Cart item with ID {cartItemId} not found");
        }

        [Fact(DisplayName = "Given an invalid command When retrieving cart item Then throws ValidationException")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new GetCartItemCommand(Guid.Empty); // ID inválido

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
