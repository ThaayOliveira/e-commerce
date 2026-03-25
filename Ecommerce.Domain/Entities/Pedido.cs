namespace Ecommerce.Domain.Entities;

public class Pedido
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public DateTime Data { get; set; } = DateTime.UtcNow;

    public decimal Total { get; set; }

    public List<ItemPedido> Itens { get; set; } = new();
}