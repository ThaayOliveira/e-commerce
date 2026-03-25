namespace Ecommerce.Domain.Entities;

public class Pagamento
{
    public int Id { get; set; }

    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }

    public string Status { get; set; } // PENDENTE, PAGO, CANCELADO
}