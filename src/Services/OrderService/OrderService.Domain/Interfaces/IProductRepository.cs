using OrderService.Domain.Entities;

namespace OrderService.Domain.Interfaces;


public interface IProductRepository
{
    Task<Product> GetByIdAsync(Guid productId);
}