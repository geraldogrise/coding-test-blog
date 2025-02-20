using Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetByCategory
{
    public class GetByCategoryHandler : IRequestHandler<GetByCategoryCommand, GetAllProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetByCategoryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetAllProductResult> Handle(GetByCategoryCommand request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetByCategoryAsync(
                request.Category,
                request.Page,
                request.Size,
                request.Order,
                cancellationToken
            );

            return new GetAllProductResult
            {
                TotalItems = products.TotalItems,
                TotalPages = products.TotalPages,
                Data = _mapper.Map<List<ProductDTO>>(products.Data)
            };
        }
    }
}
