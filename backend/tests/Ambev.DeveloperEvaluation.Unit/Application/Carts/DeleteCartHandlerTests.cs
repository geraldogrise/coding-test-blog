using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
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
    public class DeleteCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly DeleteCartHandler _handler;

        public DeleteCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new DeleteCartHandler(_cartRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid cart ID When deleting cart Then returns success response")]
        public async Task Handle_ValidCartId_ReturnsSuccessResponse()
        {
            // Given
            var cartId = Guid.NewGuid();
            var cart = new Cart { Id = cartId };
            var expectedResult = new GetCartResult { Id = cart.Id };

            _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);
            _cartRepository.DeleteAsync(cartId, Arg.Any<CancellationToken>()).Returns(true);
            _mapper.Map<GetCartResult>(cart).Returns(expectedResult);

            var command = new DeleteCartCommand(cartId);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(cartId);
            await _cartRepository.Received(1).DeleteAsync(cartId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given an invalid cart ID When deleting cart Then throws KeyNotFoundException")]
        public async Task Handle_InvalidCartId_ThrowsKeyNotFoundException()
        {
            // Given
            var cartId = Guid.NewGuid();
            var command = new DeleteCartCommand(cartId);

            _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns((Cart)null);
            _cartRepository.DeleteAsync(cartId, Arg.Any<CancellationToken>()).Returns(false);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Cart with ID {cartId} not found");
        }

        [Fact(DisplayName = "Given an invalid command When deleting cart Then throws ValidationException")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new DeleteCartCommand(Guid.Empty); // ID inválido

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
