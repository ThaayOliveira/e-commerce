public class ProdutoResponseDTO
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;

    public decimal Preco { get; set; }
    public int Estoque { get; set; }

    public string CategoriaNome { get; set; } = string.Empty;
}

//usado na saida get