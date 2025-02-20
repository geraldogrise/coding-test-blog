using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.CartsItems.GetAllCartItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.CartItems.GetAllCartItem;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    /// <summary>
    /// Profile for mapping between Application and API CreateCart responses.
    /// </summary>
    public class CreateCartProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateCart feature.
        /// </summary>
        public CreateCartProfile()
        {
            CreateMap<CreateCartRequest, CreateCartCommand>();
            CreateMap<CreateCartResult, CreateCartResponse>();
             CreateMap<Cart, CreateCartResult>();
        }
    }
}
