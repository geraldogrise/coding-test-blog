using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.CartsItems.CreateCartItem;
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
    public class CreateCartHandlerTests
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly CreateCartHandler _handler;

        public CreateCartHandlerTests()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreateCartHandler(_cartRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid CreateCartCommand When handling request Then returns CreateCartResult")]
        public async Task Handle_ValidCommand_ReturnsCreateCartResult()
        {
            // Given
            var command = new CreateCartCommand
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Items = new List<CreateCartItemCommand>()
            };
            var cart = new Cart { Id = command.Id, UserId = command.UserId };
            var expectedResult = new CreateCartResult { Id = cart.Id };

            _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>()).Returns(cart);
            _mapper.Map<CreateCartResult>(cart).Returns(expectedResult);

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(cart.Id);
            await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
        }

        [Fact(DisplayName = "Given an invalid CreateCartCommand When handling request Then throws ValidationException")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new CreateCartCommand { Id = Guid.Empty, UserId = Guid.Empty, Date = DateTime.UtcNow };

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
