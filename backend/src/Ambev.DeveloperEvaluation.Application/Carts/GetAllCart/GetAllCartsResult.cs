using Ambev.DeveloperEvaluation.Domain.Entities;


namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCart
{
    /// <summary>
    /// Response model for GetAllCarts operation
    /// </summary>
    public class GetAllCartsResult
    {
        /// <summary>
        /// The list of carts
        /// </summary>
        public List<Cart> Carts { get; set; } = new();

        /// <summary>
        /// The total number of items
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// The current page number
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The total number of pages
        /// </summary>
        public int TotalPages { get; set; }
    }
}
