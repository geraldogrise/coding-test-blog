using Ambev.DeveloperEvaluation.Application.CartsItems;
using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.GetAllCartItem
{
    /// <summary>
    /// API response model for GetAllCartItems operation.
    /// </summary>
    public class GetAllCartItemsResponse
    {
        /// <summary>
        /// The total number of users
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// The total number of pages
        /// </summary>
        public int TotalPages { get; set; }


        /// <summary>
        /// The list of products retrieved.
        /// </summary>
        public List<CartItemDto> Data { get; set; } = new();
    }

    
}
