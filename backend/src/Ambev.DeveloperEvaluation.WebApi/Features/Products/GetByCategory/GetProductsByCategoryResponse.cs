using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByCategory
{
    /// <summary>
    /// API response model for GetProductsByCategory operation.
    /// </summary>
    public class GetProductsByCategoryResponse
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
        public List<ProductDTO> Data { get; set; } = new();
    }
}
