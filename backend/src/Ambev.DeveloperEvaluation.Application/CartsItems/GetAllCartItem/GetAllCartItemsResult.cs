using Ambev.DeveloperEvaluation.Application.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.CartsItems.GetAllCartItem
{
    /// <summary>
    /// Response model for GetAllCartItems operation.
    /// </summary>
    public class GetAllCartItemsResult
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
