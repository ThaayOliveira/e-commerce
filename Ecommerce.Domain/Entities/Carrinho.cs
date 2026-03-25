namespace Ecommerce.Domain.Entities;

public class Carrinho
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public List<ItemCarrinho> Itens { get; set; } = new();
}