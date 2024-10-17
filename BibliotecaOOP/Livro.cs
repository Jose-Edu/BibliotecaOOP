using Microsoft.VisualBasic;

public class Livro : ItemBiblioteca 
{

    public string Autor { get; private set; }
    public string Isbn { get; private set; }
    public string Genero { get; private set; }
    public int Estoque { get; private set; }

    public Livro(string Titulo, string Autor, string Isbn, string Genero, int Estoque) : base(Titulo) 
    {

        this.Titulo = Titulo;
        this.Autor = Autor;
        this.Isbn = Isbn;
        this.Genero = Genero;
        this.Estoque = Estoque;

    }

    public override void Emprestimo(int dias){}

    public override void Devolucao(){}

    public override void ExibirInfo()   
    {
        Console.WriteLine($"| Título: {Titulo}, Autor: {Autor}, Isbn: {Isbn}, Gênero: {Genero}, Quantidade em estoque: {Estoque}.| ");
    }

}