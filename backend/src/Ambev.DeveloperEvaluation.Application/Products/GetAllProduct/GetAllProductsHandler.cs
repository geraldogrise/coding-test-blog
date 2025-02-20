using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Users.DTOS;
using Ambev.DeveloperEvaluation.Application.Users.GetAllUser;
using Ambev.DeveloperEvaluation.Common.Result;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, GetAllProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetAllProductResult> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(request.Page, request.Size, request.Order, cancellationToken);

            var result = new GetAllProductResult()
            {
                TotalItems = products.TotalItems,
                TotalPages = products.TotalPages,
                Data = _mapper.Map<List<ProductDTO>>(products.Data.ToList())
            };

            return _mapper.Map<GetAllProductResult>(result);

        }
    }
}