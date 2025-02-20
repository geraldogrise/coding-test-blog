using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using AutoMapper;
using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product with ID {request.Id} not found");

            // Atualiza os campos
            existingProduct.Title = request.Title;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;

            var updatedProduct = await _productRepository.UpdateAsync(request.Id, existingProduct, cancellationToken);

            return _mapper.Map<UpdateProductResult>(updatedProduct);
        }
    }
}
