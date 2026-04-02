namespace Ecommerce.Domain.Entities;

public class Produto
{
    public int Id { get; private set; }

    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public decimal Preco { get; private set; }
    public int Estoque { get; private set; }

    public int CategoriaId { get; private set; }
    public Categoria Categoria { get; private set; } = null!;

    public Produto(string nome, string descricao, decimal preco, int estoque, int categoriaId)
    {
        Validar(nome, descricao, preco, estoque, categoriaId);

        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Estoque = estoque;
        CategoriaId = categoriaId;
    }

    public void Atualizar(string nome, string descricao, decimal preco, int estoque, int categoriaId)
    {
        Validar(nome, descricao, preco, estoque, categoriaId);

        Nome = nome;
        Descricao = descricao;
        Preco = preco;
        Estoque = estoque;
        CategoriaId = categoriaId;
    }

    private static void Validar(string nome, string descricao, decimal preco, int estoque, int categoriaId)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome é obrigatório");

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória");

        if (preco <= 0)
            throw new ArgumentException("Preço deve ser maior que zero");

        if (estoque < 0)
            throw new ArgumentException("Estoque não pode ser negativo");

        if (categoriaId <= 0)
            throw new ArgumentException("Categoria inválida");
    }

    private Produto() { }
}