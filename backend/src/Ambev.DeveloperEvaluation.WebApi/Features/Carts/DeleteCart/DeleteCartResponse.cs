using Ambev.DeveloperEvaluation.WebApi.Features.CartItems.CreateCartItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart
{
    public class DeleteCartResponse
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
