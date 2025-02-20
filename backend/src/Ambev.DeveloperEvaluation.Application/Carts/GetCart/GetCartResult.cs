using Ambev.DeveloperEvaluation.Application.CartsItems.GetCartItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{

    /// <summary>
    /// Response model for GetCart operation
    /// </summary>
    public class GetCartResult
    {
        /// <summary>
        /// The unique identifier of the cart
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The user's unique identifier who owns the cart
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// The total price of all items in the cart
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// List of cart items
        /// </summary>
        public List<GetCartItemResult> Items { get; set; } = new();
    }
}
