using Ecommerce.Application.DTOs;
namespace Ecommerce.Application.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoResponseDTO>> GetAllAsync();
    Task<ProdutoResponseDTO> GetByIdAsync(int id);
    Task<ProdutoResponseDTO> CreateAsync(ProdutoCreateDTO dto);
    Task UpdateAsync(int id, ProdutoCreateDTO dto);
    

    Task DeleteAsync(int id);
}