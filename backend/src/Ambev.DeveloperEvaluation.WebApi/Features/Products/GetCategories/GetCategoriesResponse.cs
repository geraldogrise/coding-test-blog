namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetCategories
{
    /// <summary>
    /// API response model for GetCategories operation.
    /// </summary>
    public class GetCategoriesResponse
    {
        /// <summary>
        /// The list of available product categories.
        /// </summary>
        public List<string> Categories { get; set; } = new();
    }
}
