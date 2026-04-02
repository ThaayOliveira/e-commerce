namespace Ecommerce.Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public string Email { get; private set; } = string.Empty;
    public string Senha { get; private set; } = string.Empty;

    public Usuario(string nome, string email, string senha)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
    }

    private Usuario() { }
}