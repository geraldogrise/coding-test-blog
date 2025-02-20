using Ambev.DeveloperEvaluation.Application.CartsItems.DeleteCartItem;
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
    public class DeleteCartItemHandlerTests
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly DeleteCartItemHandler _handler;

        public DeleteCartItemHandlerTests()
        {
            _cartItemRepository = Substitute.For<ICartItemRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new DeleteCartItemHandler(_cartItemRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid cart item ID When deleting Then returns success response")]
        public async Task Handle_ValidCartItemId_ReturnsSuccessResponse()
        {
            // Given
            var cartItemId = Guid.NewGuid();
            var cartItem = new Ambev.DeveloperEvaluation.Domain.Entities.CartItem { Id = cartItemId };
            var expectedResult = new GetCartItemResult { Id = cartItem.Id };

            _cartItemRepository.GetByIdAsync(cartItemId, Arg.Any<CancellationToken>()).Returns(cartItem);
            _cartItemRepository.DeleteAsync(cartItemId, Arg.Any<CancellationToken>()).Returns(Task.FromResult(true));
            _mapper.Map<GetCartItemResult>(cartItem).Returns(expectedResult);

            var command = new DeleteCartItemCommand(cartItemId);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(cartItemId);
            await _cartItemRepository.Received(1).DeleteAsync(cartItemId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given an invalid cart item ID When deleting Then throws KeyNotFoundException")]
        public async Task Handle_InvalidCartItemId_ThrowsKeyNotFoundException()
        {
            // Given
            var cartItemId = Guid.NewGuid();
            var command = new DeleteCartItemCommand(cartItemId);

            _cartItemRepository.GetByIdAsync(cartItemId, Arg.Any<CancellationToken>()).Returns((Ambev.DeveloperEvaluation.Domain.Entities.CartItem)null);
            _cartItemRepository.DeleteAsync(cartItemId, Arg.Any<CancellationToken>()).Returns(Task.FromResult(false));

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Cart item with ID {cartItemId} not found");
        }

        [Fact(DisplayName = "Given an invalid command When deleting Then throws ValidationException")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new DeleteCartItemCommand(Guid.Empty); // ID inválido

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
