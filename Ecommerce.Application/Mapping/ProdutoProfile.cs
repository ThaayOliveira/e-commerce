using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappings;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<Produto, ProdutoResponseDTO>()
            .ForMember(dest => dest.CategoriaNome,
                opt => opt.MapFrom(src => src.Categoria != null ? src.Categoria.Nome : ""));
    }
}