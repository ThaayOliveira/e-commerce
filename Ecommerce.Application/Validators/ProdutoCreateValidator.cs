using FluentValidation;
using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Validators;

public class ProdutoCreateValidator : AbstractValidator<ProdutoCreateDTO>
{
    public ProdutoCreateValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("Descrição é obrigatória");

        RuleFor(x => x.Preco)
            .GreaterThan(0).WithMessage("Preço deve ser maior que zero");

        RuleFor(x => x.Estoque)
            .GreaterThanOrEqualTo(0).WithMessage("Estoque não pode ser negativo");

        RuleFor(x => x.CategoriaId)
            .GreaterThan(0).WithMessage("CategoriaId inválido");
    }
}