using Ambev.DeveloperEvaluation.Common.Result;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : IProductRepository
{
    private readonly DefaultContext _context;

    public ProductRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task<Product> UpdateAsync(Guid id, Product product, CancellationToken cancellationToken = default)
    {
        var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (existingProduct == null)
            return null;

        // Update fields
        existingProduct.Title = product.Title;
        existingProduct.Price = product.Price;
        existingProduct.Description = product.Description;
        existingProduct.Category = product.Category;
        existingProduct.Image = product.Image;
        existingProduct.Rating = product.Rating;

        await _context.SaveChangesAsync(cancellationToken);
        return existingProduct;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products.ToListAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await GetByIdAsync(id, cancellationToken);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<Ambev.DeveloperEvaluation.Common.Result.PagedResult<Product>> GetAllAsync(int page, int size, string order, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.AsQueryable();

        // Apply ordering
        if (!string.IsNullOrEmpty(order))
        {
            query = query.OrderBy(order);
        }

        // Get paginated result
        var totalItems = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling((double)totalItems / size);

        var products = await query.Skip((page - 1) * size)
                                  .Take(size)
                                  .ToListAsync(cancellationToken);

        return new Ambev.DeveloperEvaluation.Common.Result.PagedResult<Product>
        {
            Data = products,
            TotalItems = totalItems,
            CurrentPage = page,
            TotalPages = totalPages
        };
    }

    public async Task<IEnumerable<string>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
                             .Select(p => p.Category)
                             .Distinct()
                             .ToListAsync(cancellationToken);
    }

    public async Task<Ambev.DeveloperEvaluation.Common.Result.PagedResult<Product>> GetByCategoryAsync(string category, int page, int size, string order, CancellationToken cancellationToken = default)
    {
        var query = _context.Products.Where(p => p.Category == category).AsQueryable();

        // Apply ordering
        if (!string.IsNullOrEmpty(order))
        {
            query = query.OrderBy(order);
        }

        // Get paginated result
        var totalItems = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling((double)totalItems / size);

        var products = await query.Skip((page - 1) * size)
                                  .Take(size)
                                  .ToListAsync(cancellationToken);

        return new Ambev.DeveloperEvaluation.Common.Result.PagedResult<Product>
        {
            Data = products,
            TotalItems = totalItems,
            CurrentPage = page,
            TotalPages = totalPages
        };
    }
}


