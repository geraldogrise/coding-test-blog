using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    /// <summary>
    /// Handler for processing DeleteCartCommand requests
    /// </summary>
    public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, GetCartResult>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of DeleteCartHandler
        /// </summary>
        /// <param name="cartRepository">The cart repository</param>
        public DeleteCartHandler(ICartRepository cartRepository,
            IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the DeleteCartCommand request
        /// </summary>
        /// <param name="request">The DeleteCart command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the delete operation</returns>
        public async Task<GetCartResult> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCartValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var cart = await _cartRepository.GetByIdAsync(request.Id, cancellationToken);
            var success = await _cartRepository.DeleteAsync(request.Id, cancellationToken);
            if (!success)
                throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

            return _mapper.Map<GetCartResult>(cart);
        }
    }
}
