﻿using Ambev.DeveloperEvaluation.Application.CartsItems.GetCartItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace Ambev.DeveloperEvaluation.Application.CartsItems.DeleteCartItem
{

    /// <summary>
    /// Handler for processing DeleteCartItemCommand requests
    /// </summary>
    public class DeleteCartItemHandler : IRequestHandler<DeleteCartItemCommand, GetCartItemResult>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of DeleteCartItemHandler
        /// </summary>
        /// <param name="cartItemRepository">The cart item repository</param>
        public DeleteCartItemHandler(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the DeleteCartItemCommand request
        /// </summary>
        /// <param name="request">The DeleteCartItem command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the delete operation</returns>
        public async Task<GetCartItemResult> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCartItemValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);
            var item = await _cartItemRepository.GetByIdAsync(request.Id, cancellationToken);
            var success = await _cartItemRepository.DeleteAsync(request.Id, cancellationToken);
            if (!success)
                throw new KeyNotFoundException($"Cart item with ID {request.Id} not found");

            return _mapper.Map<GetCartItemResult>(item);
        }
    }
}
