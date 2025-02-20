using Ambev.DeveloperEvaluation.WebApi.Features.Products.Requests;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    /// <summary>
    /// Represents a request to create a new product in the system.
    /// </summary>
    public class CreateProductRequest
    {

        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public int Guid { get; set; }

        /// <summary>
        /// Gets or sets the product title.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product category.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product image URL.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product rating.
        /// </summary>
        public RatingRequest Rating { get; set; } = new RatingRequest();
    }
}
