using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.CartsItems;
using Ambev.DeveloperEvaluation.Application.CartsItems.GetAllCartItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.GetAllCartItem
{

    public class GetAllCartItemsProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for CreateCart feature.
        /// </summary>
        public GetAllCartItemsProfile()
        {
            CreateMap<GetAllCartItemsRequest, GetAllCartItemsCommand>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<GetAllCartItemsResult, GetAllCartItemsResponse>().ReverseMap();
        }
    }

}
