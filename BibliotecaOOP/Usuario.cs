public class Usuario : IPesquisavel
{
    public string Nome { get; private set; }
    public int Id { get; private set; }
    public string Contato { get; private set; }
    public string Endereco { get; private set; }

    public Usuario(string Nome, int Id, string Contato, string Endereco)
    {
        this.Nome = Nome;
        this.Id = Id;
        this.Contato = Contato;
        this.Endereco = Endereco;
    }

    public void ExibirInfo() 
    {
        Console.WriteLine($"| Nome: {Nome}, Id: {Id}, Contato: {Contato}, Endereço: {Endereco}. |");
    }

}