using Microsoft.EntityFrameworkCore;
using DotNetMicroDemo.Models;
using Polly;
using Polly.Extensions.Http;

namespace DotNetMicroDemo.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> CreateProductAsync(Product product);
    Task<Product?> UpdateProductAsync(int id, Product product);
    Task<bool> DeleteProductAsync(int id);
}

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly IAsyncPolicy _retryPolicy;
    private readonly ILogger<ProductService> _logger;

    public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
    {
        _context = context;
        _logger = logger;

        // Retry policy for database operations
        _retryPolicy = Policy
            .Handle<Microsoft.Data.Sqlite.SqliteException>()
            .Or<TimeoutException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryCount, context) =>
                {
                    _logger.LogWarning("Retry {RetryCount} for database operation after {Delay}ms",
                        retryCount, timespan.TotalMilliseconds);
                });
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Fetching all products");
            return await _context.Products.ToListAsync();
        });
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Fetching product with ID: {ProductId}", id);
            return await _context.Products.FindAsync(id);
        });
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Creating new product: {ProductName}", product.Name);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        });
    }

    public async Task<Product?> UpdateProductAsync(int id, Product product)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Updating product with ID: {ProductId}", id);
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return null;

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;

            await _context.SaveChangesAsync();
            return existingProduct;
        });
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Deleting product with ID: {ProductId}", id);
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        });
    }
}