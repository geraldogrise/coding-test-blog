using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    /// <summary>
    /// Profile for mapping between Product entity and CreateProductResponse
    /// </summary>
    public class CreateProductProfile : Profile
    {
        public CreateProductProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<Product, CreateProductResult>();
            CreateMap<RatingDto, RatttingDto>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
