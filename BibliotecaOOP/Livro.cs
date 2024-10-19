public class Livro : ItemBiblioteca 
{

    public string Autor { get; set; }
    public string Isbn { get; set; }
    public string Genero { get; set; }
    public int Quantidade { get; set; }

    public Livro(string Titulo, string Codigo, string Autor, string Genero, int Quantidade) : base(Titulo) 
    {

        this.Titulo = Titulo;
        this.Codigo = Codigo;
        Isbn = Codigo;
        this.Autor = Autor;
        this.Genero = Genero;
        this.Quantidade = Quantidade;

    }

    public override void Emprestimo(Usuario usuario, int dias)
    {
        
    }

    public override void Devolucao(Usuario usuario)
    {

    }

    public override void ExibirInfo()   
    {
        Console.WriteLine($"| Título: {Titulo}, Código ISBN: {Codigo}, Autor: {Autor}, Gênero: {Genero}, Quantidade disponível: {Quantidade}. |");
    }

}