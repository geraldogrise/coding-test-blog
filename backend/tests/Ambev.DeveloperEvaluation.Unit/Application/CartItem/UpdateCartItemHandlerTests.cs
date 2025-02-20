using Ambev.DeveloperEvaluation.Application.CartsItems.UpdateCartItem;
using Ambev.DeveloperEvaluation.Common.Result;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.Linq.Dynamic.Core;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.CartItem
{
    public class UpdateCartItemHandlerTests
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly UpdateCartItemHandler _handler;

        public UpdateCartItemHandlerTests()
        {
            _cartItemRepository = new InMemoryCartItemRepository(); // Implementação fictícia de repositório
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<Ambev.DeveloperEvaluation.Domain.Entities.CartItem, UpdateCartItemResult>()).CreateMapper();
            _handler = new UpdateCartItemHandler(_cartItemRepository, _mapper);
        }

        [Fact(DisplayName = "Given a valid UpdateCartItemCommand When handling request Then returns UpdateCartItemResult")]
        public async Task Handle_ValidCommand_ReturnsUpdateCartItemResult()
        {
            // Given
            var existingItem = new Ambev.DeveloperEvaluation.Domain.Entities.CartItem
            {
                Id = Guid.NewGuid(),
                CartId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 1
            };
            await _cartItemRepository.CreateAsync(existingItem, CancellationToken.None);

            var command = new UpdateCartItemCommand
            {
                Id = existingItem.Id,
                CartId = existingItem.CartId,
                ProductId = existingItem.ProductId,
                Quantity = 5 // Atualizando a quantidade
            };

            // When
            var result = await _handler.Handle(command, CancellationToken.None);

            // Then
            result.Should().NotBeNull();
            result.Id.Should().Be(existingItem.Id);
            result.Quantity.Should().Be(5);
        }

        [Fact(DisplayName = "Given an invalid UpdateCartItemCommand When handling request Then throws ValidationException")]
        public async Task Handle_InvalidCommand_ThrowsValidationException()
        {
            // Given
            var command = new UpdateCartItemCommand { Id = Guid.Empty, CartId = Guid.Empty, ProductId = Guid.Empty, Quantity = 0 };

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<ValidationException>();
        }

        [Fact(DisplayName = "Given a non-existing cart item When handling request Then throws KeyNotFoundException")]
        public async Task Handle_NonExistingCartItem_ThrowsKeyNotFoundException()
        {
            // Given
            var command = new UpdateCartItemCommand
            {
                Id = Guid.NewGuid(),
                CartId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                Quantity = 3
            };

            // When
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Then
            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }

    // Implementação fictícia de um repositório em memória para testes
    public class InMemoryCartItemRepository : ICartItemRepository
    {
        private readonly Dictionary<Guid, Ambev.DeveloperEvaluation.Domain.Entities.CartItem> _items = new();

        public Task<Ambev.DeveloperEvaluation.Domain.Entities.CartItem> GetByIdAsync(Guid id, CancellationToken cancellationToken)
            => Task.FromResult(_items.ContainsKey(id) ? _items[id] : null);

        public Task<Ambev.DeveloperEvaluation.Domain.Entities.CartItem> CreateAsync(Ambev.DeveloperEvaluation.Domain.Entities.CartItem item, CancellationToken cancellationToken)
        {
            _items[item.Id] = item;
            return Task.FromResult(item);
        }

        public Task<Ambev.DeveloperEvaluation.Domain.Entities.CartItem> UpdateAsync(Guid id, Ambev.DeveloperEvaluation.Domain.Entities.CartItem item, CancellationToken cancellationToken)
        {
            if (!_items.ContainsKey(id)) throw new KeyNotFoundException();
            _items[id] = item;
            return Task.FromResult(item);
        }

        public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            if (!_items.ContainsKey(id)) return Task.FromResult(false);
            _items.Remove(id);
            return Task.FromResult(true);
        }

        public Task<Ambev.DeveloperEvaluation.Common.Result.PagedResult<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken)
        {
            var query = _items.Values.AsQueryable();

            if (!string.IsNullOrWhiteSpace(order))
            {
                query = query.OrderBy(order);
            }

            int totalItems = query.Count();
            var pagedItems = query.Skip((page - 1) * size).Take(size).ToList();

            return Task.FromResult(new Ambev.DeveloperEvaluation.Common.Result.PagedResult<Ambev.DeveloperEvaluation.Domain.Entities.CartItem>
            {
                Data = pagedItems,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)size)
            });
        }
    }
}
