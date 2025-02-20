using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.CartsItems.CreateCartItem;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{

    public class CreateCartItemProfile : Profile
    {
        public CreateCartItemProfile()
        {
            CreateMap<CartItem, CreateCartItemResult>();
        }
    }
}
