using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.HetCategories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesCommand, List<string>>
    {
        private readonly IProductRepository _productRepository;

        public GetCategoriesHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<string>> Handle(GetCategoriesCommand request, CancellationToken cancellationToken)
        {
            return (List<string>) await _productRepository.GetCategoriesAsync(cancellationToken);
        }
    }
}
