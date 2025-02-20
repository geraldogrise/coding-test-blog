using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    /// <summary>
    /// Profile for mapping UpdateProduct feature requests to commands.
    /// </summary>
    public class UpdateProductProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for UpdateProduct feature.
        /// </summary>
        public UpdateProductProfile()
        {
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
            CreateMap<UpdateProductResult, UpdateProductResponse>();
        }
    }
}
