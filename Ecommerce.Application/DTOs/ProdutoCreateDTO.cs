namespace Ecommerce.Application.DTOs;

public class ProdutoCreateDTO
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public int CategoriaId { get; set; }
}