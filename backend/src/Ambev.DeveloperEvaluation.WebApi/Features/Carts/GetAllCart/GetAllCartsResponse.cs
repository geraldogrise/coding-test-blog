using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Features.CartItems.CreateCartItem;
using Ambev.DeveloperEvaluation.WebApi.Features.CartItems.GetCartItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCart
{
    /// <summary>
    /// API response model for GetAllCarts operation
    /// </summary>
    public class GetAllCartsResponse
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the cart.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The list of items in the cart.
        /// </summary>
        public List<CreateCartItemResponse> Items { get; set; } = new();

    }
}
