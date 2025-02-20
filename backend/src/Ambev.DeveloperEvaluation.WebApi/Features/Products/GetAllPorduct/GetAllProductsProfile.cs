using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;
using Ambev.DeveloperEvaluation.Application.Users.DTOS;
using Ambev.DeveloperEvaluation.Application.Users.GetAllUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllPorduct.Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllPorduct
{
    /// <summary>
    /// Profile for mapping GetAllProducts feature requests to commands.
    /// </summary>
    public class GetAllProductsProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetAllProducts feature.
        /// </summary>
        public GetAllProductsProfile()
        {
            CreateMap<GetAllProductsRequest, GetAllProductsCommand>();
            CreateMap<GetAllProductResult, GetAllProductsResponse>()
    .        ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data)).ReverseMap();
          

        }
    }
}
