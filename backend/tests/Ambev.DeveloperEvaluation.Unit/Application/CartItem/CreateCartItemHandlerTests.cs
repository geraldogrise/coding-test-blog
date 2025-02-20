using Ambev.DeveloperEvaluation.Application.CartsItems.CreateCartItem;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.CartItem
{
    public class CreateCartItemHandlerTests
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly CreateCartItemHandler _handler;

        public CreateCartItemHandlerTests()
        {
            _cartItemRepository = Substitute.For<ICartItemRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateCartItemHandler(_cartItemRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid CreateCartItemCommand When handling request Then returns CreateCartItemResult")]
        public async Task Handle_ValidCommand_ReturnsCreateCartItemResult()
        {
            // Given
            var command = new CreateCartItemCommand
            {
                CartId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 2
            };
            var cartItem = new Ambev.DeveloperEvaluation.Domain.Entities.CartItem
            {
                Id = Guid.NewGuid(),
                CartId = command.CartId,
                ProductId = command.ProductId,
                Quantity = command.Quantity
            };
            var expectedResult = new CreateCartItemResult { CartId = cartItem.CartId, ProductId = cartItem.ProductId, Quantity = cartItem.Quantity };

            _cartItemRepository.CreateAsync(Arg.Any<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>(), Arg.Any<CancellationToken>()).Returns(cartItem);
            _mapper.Map<CreateCartItemResult>(cartItem).Returns(expectedResult);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.CartId.Should().Be(cartItem.CartId);
            await _cartItemRepository.Received(1).CreateAsync(Arg.Any<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given an invalid CreateCartItemCommand When handling request Then throws ValidationException")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new CreateCartItemCommand { CartId = Guid.Empty, ProductId = Guid.Empty, Quantity = 0 };

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
