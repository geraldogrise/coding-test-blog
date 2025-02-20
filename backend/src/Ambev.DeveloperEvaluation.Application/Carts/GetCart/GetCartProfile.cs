﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    /// <summary>
    /// Profile for mapping between Cart entity and GetCartResponse
    /// </summary>
    public class GetCartProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetCart operation
        /// </summary>
        public GetCartProfile()
        {
            CreateMap<Cart, GetCartResult>();
        }
    }
}
