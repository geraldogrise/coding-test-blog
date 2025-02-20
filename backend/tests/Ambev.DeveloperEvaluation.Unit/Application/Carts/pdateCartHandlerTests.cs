using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
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

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts
{
    public class UpdateCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly UpdateCartHandler _handler;

        public UpdateCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateCartHandler(_cartRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid cart update request When updating cart Then returns updated cart result")]
        public async Task Handle_ValidRequest_ReturnsUpdatedCartResult()
        {
            // Given
            var cartId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var request = new UpdateCartCommand
            {
                Id = cartId,
                UserId = userId,
                Date = DateTime.UtcNow,
                Items = new List<UpdateCartItemDto>
                {
                    new() { ProductId = Guid.NewGuid(), Quantity = 2 },
                    new() { ProductId = Guid.NewGuid(), Quantity = 5 }
                }
            };

            var cart = new Cart { Id = cartId, UserId = userId, Items = new List<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>() };
            var updatedCart = new Cart { Id = cartId, UserId = userId, Items = request.Items.Select(i => new Ambev.DeveloperEvaluation.Domain.Entities.CartItem { ProductId = i.ProductId, Quantity = i.Quantity }).ToList() };
            var expectedResult = new UpdateCartResult();

            _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);
            _cartRepository.UpdateAsync(cartId, Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(updatedCart);
            _mapper.Map<UpdateCartResult>(updatedCart).Returns(expectedResult);

            // When
            var result = await _handler.Handle(request, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
            await _cartRepository.Received(1).UpdateAsync(cartId, Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given a non-existent cart ID When updating cart Then throws KeyNotFoundException")]
        public async Task Handle_NonExistentCartId_ThrowsKeyNotFoundException()
        {
            // Given
            var cartId = Guid.NewGuid();
            var request = new UpdateCartCommand { Id = cartId, UserId = Guid.NewGuid(), Date = DateTime.UtcNow, Items = new List<UpdateCartItemDto>() };

            _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns((Cart)null);

            // When
            var act = () => _handler.Handle(request, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Cart with ID {cartId} not found.");
        }

        [Fact(DisplayName = "Given an invalid update request When updating cart Then throws ValidationException")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Given
            var request = new UpdateCartCommand { Id = Guid.Empty, UserId = Guid.NewGuid(), Date = DateTime.UtcNow, Items = new List<UpdateCartItemDto>() };

            // When
            var act = () => _handler.Handle(request, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
