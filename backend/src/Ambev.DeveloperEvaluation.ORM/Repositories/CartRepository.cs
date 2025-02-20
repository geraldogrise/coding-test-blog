using Ambev.DeveloperEvaluation.Common.Result;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of ICartRepository using Entity Framework Core
/// </summary>
public class CartRepository : ICartRepository
{
    private readonly DefaultContext _context;

    public CartRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return cart;
    }


    public async Task<Cart> UpdateAsync(Guid id, Cart cart, CancellationToken cancellationToken = default)
    {
        var existingCart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (existingCart == null)
            throw new KeyNotFoundException($"Cart with ID {id} not found.");

        // Atualiza as propriedades do carrinho
        existingCart.UserId = cart.UserId;

        // Remove os itens antigos e adiciona os novos
        _context.CartItems.RemoveRange(existingCart.Items);
        existingCart.Items = cart.Items;

        await _context.SaveChangesAsync(cancellationToken);
        return existingCart;
    }

    public async Task<Cart?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cart = await GetByIdAsync(id, cancellationToken);
        if (cart == null)
            return false;

        _context.Carts.Remove(cart);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }


    /// <summary>
    /// Retrieves all carts with pagination and ordering
    /// </summary>
    public async Task<Ambev.DeveloperEvaluation.Common.Result.PagedResult<Cart>> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default)
    {
        var query = _context.Carts
            .Include(c => c.Items)
            .ThenInclude(ci => ci.Product)
            .AsQueryable();

        // Ordenação dinâmica
        if (!string.IsNullOrWhiteSpace(order))
        {
            query = query.OrderBy(order);
        }

        // Paginação
        int totalItems = await query.CountAsync(cancellationToken);
        List<Cart> carts = await query
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return new Ambev.DeveloperEvaluation.Common.Result.PagedResult<Cart>
        {
            Data = carts,
            TotalItems = totalItems,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalItems / (double)size)
        };
    }
}
