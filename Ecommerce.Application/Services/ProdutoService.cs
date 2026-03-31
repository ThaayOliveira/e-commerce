using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappings;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using AutoMapper;

namespace Ecommerce.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repository;
    private readonly IMapper _mapper;

    public ProdutoService(IProdutoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoResponseDTO>> GetAllAsync()
    {
        var produtos = await _repository.GetAllAsync();
        
        return _mapper.Map<IEnumerable<ProdutoResponseDTO>>(produtos);
    }

    public async Task<ProdutoResponseDTO> GetByIdAsync(int id)
    {
        var produto = await _repository.GetByIdAsync(id);

        if (produto == null)
            throw new NotFoundException("Produto não encontrado");

        return _mapper.Map<ProdutoResponseDTO>(produto);
    }

    public async Task<ProdutoResponseDTO> CreateAsync(ProdutoCreateDTO dto)
    {
        var categoriaExiste = await _repository.CategoriaExistsAsync(dto.CategoriaId);

        if (!categoriaExiste)
            throw new NotFoundException("Categoria não encontrada");

        var produto = new Produto(
            dto.Nome,
            dto.Descricao,
            dto.Preco,
            dto.Estoque,
            dto.CategoriaId
        );

        await _repository.AddAsync(produto);

        return _mapper.Map<ProdutoResponseDTO>(produto);
    }

    public async Task UpdateAsync(int id, ProdutoCreateDTO dto)
    {
        var produto = await _repository.GetByIdAsync(id);

        if (produto == null)
            throw new NotFoundException("Produto não encontrado");

        var categoriaExiste = await _repository.CategoriaExistsAsync(dto.CategoriaId);

        if (!categoriaExiste)
            throw new NotFoundException("Categoria não encontrada");

        produto.Atualizar(
            dto.Nome,
            dto.Descricao,
            dto.Preco,
            dto.Estoque,
            dto.CategoriaId
        );

        await _repository.UpdateAsync(produto);
    }

    public async Task DeleteAsync(int id)
    {
        var produto = await _repository.GetByIdAsync(id);

        if (produto == null)
            throw new NotFoundException("Produto não encontrado");

        await _repository.DeleteAsync(produto);
    }
}