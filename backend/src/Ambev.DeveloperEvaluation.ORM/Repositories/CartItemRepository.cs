using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ICartItemRepository using Entity Framework Core
/// </summary>
public class CartItemRepository : ICartItemRepository
{
    private readonly DefaultContext _context;

    public CartItemRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<CartItem> CreateAsync(CartItem cartItem, CancellationToken cancellationToken = default)
    {
        await _context.CartItems.AddAsync(cartItem, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cartItem;
    }

    /// <summary>
    /// Updates an existing cart item
    /// </summary>
    public async Task<CartItem> UpdateAsync(Guid id, CartItem cartItem, CancellationToken cancellationToken = default)
    {
        var existingItem = await _context.CartItems
            .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);

        if (existingItem == null)
            throw new KeyNotFoundException($"Cart item with ID {id} not found.");

        // Atualiza os dados do item
        existingItem.Quantity = cartItem.Quantity;

        await _context.SaveChangesAsync(cancellationToken);
        return existingItem;
    }

    public async Task<CartItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.CartItems
            .Include(ci => ci.Product)
            .FirstOrDefaultAsync(ci => ci.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cartItem = await GetByIdAsync(id, cancellationToken);
        if (cartItem == null)
            return false;

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Retrieves all cart items with pagination and ordering
    /// </summary>
    public async Task<Ambev.DeveloperEvaluation.Common.Result.PagedResult<CartItem>> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default)
    {
        var query = _context.CartItems
            .Include(ci => ci.Cart)
            .Include(ci => ci.Product)
            .AsQueryable();

        // Ordenação dinâmica
        if (!string.IsNullOrWhiteSpace(order))
        {
            query = query.OrderBy(order);
        }

        // Paginação
        int totalItems = await query.CountAsync(cancellationToken);
        List<CartItem> cartItems = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return new Ambev.DeveloperEvaluation.Common.Result.PagedResult<CartItem>
        {
            Data = cartItems,
            TotalItems = totalItems,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalItems / (double)size)
        };
    }
}
