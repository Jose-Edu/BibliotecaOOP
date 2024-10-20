using System.Data;
using System.Globalization;

public class Livro : ItemBiblioteca 
{

    public string Autor { get; set; }
    public string Isbn { get; set; }
    public string Genero { get; set; }
    public int Quantidade { get; set; }
    public static int precoPorDia = 10;
    public static int precoMultaDiaria = precoPorDia*2;

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
        if(Quantidade <= 0)
        {
            Console.WriteLine("Esse livro não possui exemplares disponíveis no momento");
            Console.ReadKey();
            Console.WriteLine();
        }
        else
        {
            DateTime dataLimite = DateTime.Now;
            dataLimite = dataLimite.AddDays(dias);

            DataBase.Query($"INSERT INTO emprestimos (user_id, book_isbn, limite) VALUES ({usuario.Id}, '{Isbn}', '{dataLimite}')");
            Quantidade--;
            DataBase.Query($"UPDATE livros SET quantidade={Quantidade} WHERE isbn={Isbn}");

            Console.WriteLine($"Emprestímo do livro {Titulo} para {usuario.Nome} realizado com sucesso!");
            Console.WriteLine($"Preço do emprestimo: {(precoPorDia*dias).ToString("C")}");
            Console.WriteLine($"Data limite de entrega: {dataLimite}");
            Console.ReadKey();
            Console.WriteLine();
        }
    }

    public override void Devolucao(Usuario usuario)
    {
        DateTime devolucao = DateTime.Now;
        string limite = Validar.String(DataBase.Read("emprestimos", $"user_id={usuario.Id} AND book_isbn='{Isbn}'").Rows[0].Field<string>("limite"));
        DateTime dataLimite = DateTime.ParseExact(limite, "dd/MM/yyyy HH:mm:ss", new CultureInfo("pt-BR"));

        Quantidade++;
        DataBase.Query($"UPDATE emprestimos SET fechamento='{devolucao}' WHERE user_id={usuario.Id} AND book_isbn='{Isbn}'");
        DataBase.Query($"UPDATE livros SET quantidade={Quantidade} WHERE isbn={Isbn}");
        
        Console.WriteLine($"Empréstimo do livro {Titulo} para {usuario.Nome} fechado com sucesso!");

        if(devolucao > dataLimite)
        {
            Console.WriteLine($"O empréstimo foi fechado com atraso, a multa é de {((devolucao-dataLimite).Days*precoMultaDiaria).ToString("C")}");
        };

        Console.ReadKey();
        Console.WriteLine();

    }

    public override void ExibirInfo()   
    {
        Console.WriteLine($"| Título: {Titulo}, Código ISBN: {Codigo}, Autor: {Autor}, Gênero: {Genero}, Quantidade disponível: {Quantidade}. |");
    }

}