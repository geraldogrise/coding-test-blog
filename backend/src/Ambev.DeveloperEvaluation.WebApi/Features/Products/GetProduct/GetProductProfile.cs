using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{

    /// <summary>
    /// Profile for mapping GetProduct feature requests to commands
    /// </summary>
    public class GetProductProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetProduct feature
        /// </summary>
        public GetProductProfile()
        {
            CreateMap<Guid, Application.Products.GetProduct.GetProductCommand>()
                .ConstructUsing(id => new Application.Products.GetProduct.GetProductCommand(id));
            CreateMap<Product, GetProductResult>();
            CreateMap<GetProductResult, GetProductResponse>();
        }
    }

}
