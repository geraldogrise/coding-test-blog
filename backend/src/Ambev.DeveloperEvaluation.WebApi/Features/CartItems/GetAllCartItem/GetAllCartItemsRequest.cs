namespace Ambev.DeveloperEvaluation.WebApi.Features.CartItems.GetAllCartItem
{
    /// <summary>
    /// Request model for retrieving all cart items with pagination and sorting.
    /// </summary>
    public class GetAllCartItemsRequest
    {
        /// <summary>
        /// Número da página (padrão: 1).
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Número de itens por página (padrão: 10).
        /// </summary>
        public int Size { get; set; } = 10;

        /// <summary>
        /// Campo de ordenação opcional (ex: "Quantity DESC").
        /// </summary>
        public string? Order { get; set; }
    }
}
