namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByCategory
{
    /// <summary>
    /// Request model for retrieving products by category.
    /// </summary>
    public class GetProductsByCategoryRequest
    {
        /// <summary>
        /// The category of the products to retrieve.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// The page number to retrieve (for pagination).
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public int Size { get; set; } = 10;

        /// <summary>
        /// The sorting order (e.g., ASC or DESC).
        /// </summary>
        public string Order { get; set; } = "ASC";
    }
}
