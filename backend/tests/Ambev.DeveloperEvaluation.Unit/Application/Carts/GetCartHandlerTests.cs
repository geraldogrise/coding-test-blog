using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
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
    public class GetCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly GetCartHandler _handler;

        public GetCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetCartHandler(_cartRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid cart ID When retrieving cart Then returns expected result")]
        public async Task Handle_ValidCartId_ReturnsExpectedResult()
        {
            // Given
            var cartId = Guid.NewGuid();
            var cart = new Cart { Id = cartId, UserId = Guid.NewGuid() };
            var expectedResult = new GetCartResult { Id = cartId };

            _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);
            _mapper.Map<GetCartResult>(cart).Returns(expectedResult);

            var command = new GetCartCommand(cartId);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(cartId);
            await _cartRepository.Received(1).GetByIdAsync(cartId, Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given an invalid cart ID When retrieving cart Then throws KeyNotFoundException")]
        public async Task Handle_InvalidCartId_ThrowsKeyNotFoundException()
        {
            // Given
            var cartId = Guid.NewGuid();
            var command = new GetCartCommand(cartId);

            _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns((Cart)null);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>().WithMessage($"Cart with ID {cartId} not found");
        }

        [Fact(DisplayName = "Given an invalid command When retrieving cart Then throws ValidationException")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new GetCartCommand(Guid.Empty);

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}
