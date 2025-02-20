namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.Requests
{
    public class RatingRequest
    {
        /// <summary>
        /// Gets or sets the rating value.
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the rating count.
        /// </summary>
        public int Count { get; set; }
    }
}
