using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.CartsItems.UpdateCartItem
{
    /// <summary>
    /// Handler for processing UpdateCartItemCommand requests.
    /// </summary>
    public class UpdateCartItemHandler : IRequestHandler<UpdateCartItemCommand, UpdateCartItemResult>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public UpdateCartItemHandler(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        public async Task<UpdateCartItemResult> Handle(UpdateCartItemCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateCartItemValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingItem = await _cartItemRepository.GetByIdAsync(command.Id, cancellationToken);
            if (existingItem == null)
                throw new KeyNotFoundException($"Cart item with ID {command.Id} not found.");

            // Criando um novo objeto CartItem atualizado
            var updatedCartItem = new CartItem
            {
                Id = existingItem.Id,
                CartId = existingItem.CartId, // Mantendo os valores originais
                ProductId = existingItem.ProductId, // Mantendo os valores originais
                Quantity = command.Quantity // Atualizando apenas a quantidade
            };

            var updatedItem = await _cartItemRepository.UpdateAsync(command.Id, updatedCartItem, cancellationToken);
            return _mapper.Map<UpdateCartItemResult>(updatedItem);
        }
    }
}

