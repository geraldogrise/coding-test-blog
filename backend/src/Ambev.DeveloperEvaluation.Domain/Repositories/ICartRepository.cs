using Ambev.DeveloperEvaluation.Common.Result;
using Ambev.DeveloperEvaluation.Domain.Entities;


namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Cart entity operations
/// </summary>
public interface ICartRepository
{
    /// <summary>
    /// Creates a new cart in the repository
    /// </summary>
    /// <param name="cart">The cart to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart</returns>
    Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates a specific cart by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the cart to update</param>
    /// <param name="cart">The cart data to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart</returns>
    Task<Cart> UpdateAsync(Guid id, Cart cart, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves a cart by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart if found, null otherwise</returns>
    Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes a cart from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the cart was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);


    /// <summary>
    /// Retrieves all carts with pagination and ordering
    /// </summary>
    /// <param name="page">Page number</param>
    /// <param name="size">Number of items per page</param>
    /// <param name="order">Ordering of the results</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A list of carts with pagination info</returns>
    Task<PagedResult<Cart>> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default);
}
