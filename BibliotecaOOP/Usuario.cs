public class Usuario
{
    public string Nome { get; set; }
    public int Id { get; set; }
    public string Contato { get; set; }
    public string Endereco { get; set; }

    public Usuario(string Nome, string Contato, string Endereco, int Id = -1)
    {
        this.Nome = Nome;
        this.Id = Id == -1 ? DataBase.SetUserId() : Id;
        this.Contato = Contato;
        this.Endereco = Endereco;
    }

    public void ExibirInfo() 
    {
        Console.WriteLine($"| Nome: {Nome}, Id: {Id}, Contato: {Contato}, Endereço: {Endereco}. |");
    }

    public void ExibirHistórico()
    {

    }

}