using Ambev.DeveloperEvaluation.Application.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllPorduct
{
    /// <summary>
    /// API response model for GetAllProducts operation.
    /// </summary>
    public class GetAllProductsResponse
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
