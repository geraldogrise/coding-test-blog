using Ambev.DeveloperEvaluation.Common.Result;
using Ambev.DeveloperEvaluation.Domain.Entities;


namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Product entity operations
/// </summary>
public interface IProductRepository
{
    /// <summary>
    /// Creates a new product in the repository
    /// </summary>
    /// <param name="product">The product to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created product</returns>
    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a specific product in the repository
    /// </summary>
    /// <param name="id">The unique identifier of the product to update</param>
    /// <param name="product">The product data to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product</returns>
    Task<Product> UpdateAsync(Guid id, Product product, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a product by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product if found, null otherwise</returns>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all products
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of all products</returns>
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a product from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the product was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a list of products with pagination and ordering
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of items per page</param>
    /// <param name="order">Ordering of the results</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of products with pagination info</returns>
    Task<PagedResult<Product>> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all product categories
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of product categories</returns>
    Task<IEnumerable<string>> GetCategoriesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves products in a specific category with pagination and ordering
    /// </summary>
    /// <param name="category">The category name</param>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of items per page</param>
    /// <param name="order">Ordering of the results</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of products in the specified category</returns>
    Task<PagedResult<Product>> GetByCategoryAsync(string category, int page, int size, string order, CancellationToken cancellationToken = default);

}
