using Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;
using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Common.Result;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.CartsItems.GetAllCartItem
{
    /// <summary>
    /// Handler for processing GetAllCartItemsCommand requests.
    /// </summary>
    public class GetAllCartItemsHandler : IRequestHandler<GetAllCartItemsCommand, GetAllCartItemsResult>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public GetAllCartItemsHandler(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        public async Task<GetAllCartItemsResult> Handle(GetAllCartItemsCommand request, CancellationToken cancellationToken)
        {
            var cartItemsPaged = await _cartItemRepository.GetAllAsync(request.Page, request.Size, request.Order, cancellationToken);

            var result = new GetAllCartItemsResult()
            {
                TotalItems = cartItemsPaged.TotalItems,
                TotalPages = cartItemsPaged.TotalPages,
                Data = _mapper.Map<List<CartItemDto>>(cartItemsPaged.Data.ToList())
            };

            return _mapper.Map<GetAllCartItemsResult>(result);


        }
    }
}
