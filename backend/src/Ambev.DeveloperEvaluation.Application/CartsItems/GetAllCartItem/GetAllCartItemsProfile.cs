using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.CartsItems.GetAllCartItem
{
    /// <summary>
    /// Profile for mapping between CartItem entity and GetAllCartItemsResult.
    /// </summary>
    public class GetAllCartItemsProfile : Profile
    {
        public GetAllCartItemsProfile()
        {
            CreateMap<CartItem, GetAllCartItemsResult>();
            CreateMap<CartItem, CartItemDto>().ReverseMap();

        }
    }
}
