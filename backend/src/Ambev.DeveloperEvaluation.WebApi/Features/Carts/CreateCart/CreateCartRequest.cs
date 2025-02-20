using Ambev.DeveloperEvaluation.WebApi.Features.CartItems.CreateCartItem;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{

    /// <summary>
    /// Represents a request to create a new cart in the system.
    /// </summary>
    public class CreateCartRequest
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
        /// Gets or sets the list of cart items.
        /// </summary>
        public List<CreateCartItemRequest> Items { get; set; } = new();
    }

 
}
