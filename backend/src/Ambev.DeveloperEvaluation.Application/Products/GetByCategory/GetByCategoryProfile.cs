using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;


namespace Ambev.DeveloperEvaluation.Application.Products.GetByCategory
{
    public class GetByCategoryProfile : Profile
    {
        public GetByCategoryProfile()
        {
            CreateMap<Product, GetProductResult>();
        }
    }
}
