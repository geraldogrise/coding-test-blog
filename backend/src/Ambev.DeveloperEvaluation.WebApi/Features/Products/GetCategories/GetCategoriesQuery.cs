using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories
{
    /// <summary>
    /// Query for retrieving all product categories.
    /// </summary>
    public class GetCategoriesQuery : IRequest<List<GetCategoriesResponse>>
    {
        // Como estamos buscando todas as categorias, não há necessidade de parâmetros.
    }
}
