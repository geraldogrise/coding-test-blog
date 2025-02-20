﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProduct
{
    public class GetAllProductsProfile : Profile
    {
        public GetAllProductsProfile()
        {
            CreateMap<Product, GetAllProductResult>();
        }
    }
}
