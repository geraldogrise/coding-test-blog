using Ambev.DeveloperEvaluation.Application.Products.GetByCategory;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{
    /// <summary>
    /// Handler for processing DeleteProductCommand requests
    /// </summary>
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, GetProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of DeleteProductHandler
        /// </summary>
        /// <param name="productRepository">The product repository</param>
        public DeleteProductHandler(
            IMapper mapper,
            IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        /// <summary>
        /// Handles the DeleteProductCommand request
        /// </summary>
        /// <param name="request">The DeleteProduct command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the delete operation</returns>
        public async Task<GetProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new FluentValidation.ValidationException(validationResult.Errors);

            var response = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            var success = await _productRepository.DeleteAsync(request.Id, cancellationToken);
            if (!success)
                throw new KeyNotFoundException($"Product with ID {request.Id} not found");

            return _mapper.Map<GetProductResult>(response);
   
        }
    }
}
