using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetAllAsync();
    Task<Produto?> GetByIdAsync(int id);
    Task AddAsync(Produto produto);
    Task UpdateAsync(Produto produto);
    Task DeleteAsync(Produto produto);
    Task<bool> CategoriaExistsAsync(int categoriaId);
}