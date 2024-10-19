using System.Data;

public class Biblioteca : IPesquisavel
{
    public List<Livro> Livros { get; private set; }
    public List<Usuario> Usuarios { get; private set; }

    public Biblioteca() 
    {
        Livros = new List<Livro>();
        Usuarios = new List<Usuario>();
        Console.Clear();
        Console.WriteLine("Carregando o banco de dados, aguarde...");
        
        DataTable livros = DataBase.Read("livros");
        DataTable usuarios = DataBase.Read("usuarios");

        foreach (DataRow livro in livros.Rows)
        {
            Livros.Add(new Livro(
                Validar.String(livro.Field<string>("titulo")), 
                Validar.String(livro.Field<string>("isbn")), 
                Validar.String(livro.Field<string>("autor")), 
                Validar.String(livro.Field<string>("genero")), 
                (int) livro.Field<Int64>("quantidade")));
        }

        foreach (DataRow usuario in usuarios.Rows)
        {
            Usuarios.Add(new Usuario(
                Validar.String(usuario.Field<string>("nome")), 
                Validar.String(usuario.Field<string>("contato")), 
                Validar.String(usuario.Field<string>("endereco")), 
                (int) usuario.Field<Int64>("id")));
        }

        Console.Clear();
    }

    public void ListarLivros(List<Livro>? listaDeLivros=null)
    {

        listaDeLivros ??= Livros;

        Console.Clear();
        Console.WriteLine("Livros no catálogo:");
        Console.WriteLine();
        foreach (Livro livro in listaDeLivros)
        {
            livro.ExibirInfo();
        };
        Console.ReadKey();
    }

    public void ListarUsuarios(List<Usuario>? listaDeUsuarios=null)
    {   
        listaDeUsuarios ??= Usuarios;
        Console.Clear();
        Console.WriteLine("Usuários cadastrados:");
        Console.WriteLine();
        foreach (Usuario usuario in listaDeUsuarios)
        {
            usuario.ExibirInfo();
        };
        Console.ReadKey();
    }

    public void ListarEmprestimos(bool incluirFechados = false)
    {

    }

    public void CadastrarLivro(Livro livro)
    {
        Console.WriteLine("Cadastrando livro no banco de dados, aguarde...");
        Livros.Add(livro);
        DataBase.Query($"INSERT INTO livros(isbn, titulo, autor, genero, quantidade) VALUES ('{livro.Isbn}', '{livro.Titulo}', '{livro.Autor}', '{livro.Genero}', {livro.Quantidade})");
        Console.Write($"Cadastro do livro {livro.Titulo} concluído com sucesso!");
        Console.ReadKey();
        Console.WriteLine();
    }

    public void CadastrarUsuario(Usuario usuario)
    {
        Console.WriteLine("Cadastrando usuário no banco de dados, aguarde...");
        Usuarios.Add(usuario);
        DataBase.Query($"INSERT INTO usuarios(nome, contato, endereco) VALUES ('{usuario.Nome}', '{usuario.Contato}', '{usuario.Endereco}')");
        Console.Write($"Cadastro do usuário {usuario.Nome} concluído com sucesso!");
        Console.ReadKey();
        Console.WriteLine();
    }

    public void EditarLivro(Livro livro, string?[] campos)
    {

        livro.Titulo = campos[0] ?? livro.Titulo;
        livro.Autor = campos[1] ?? livro.Autor;
        livro.Genero = campos[2] ?? livro.Genero;
        livro.Quantidade = campos[3] == null ? livro.Quantidade : Convert.ToInt32(campos[4]);

        DataBase.Query($"UPDATE usuarios SET titulo='{livro.Titulo}', autor='{livro.Autor}', genero='{livro.Genero}', quantidade={livro.Quantidade} WHERE isbn='{livro.Isbn}'");

        Console.WriteLine("Livro atualizado com sucesso!");
        livro.ExibirInfo();
        Console.ReadKey();
        
    }

    public void EditarUsuario(Usuario usuario, string?[] campos)
    {

        usuario.Nome = campos[0] ?? usuario.Nome;
        usuario.Contato = campos[1] ?? usuario.Contato;
        usuario.Endereco = campos[2] ?? usuario.Contato;

        DataBase.Query($"UPDATE usuarios SET nome = '{usuario.Nome}', contato='{usuario.Contato}',endereco='{usuario.Endereco}', WHERE id={usuario.Id}");

        Console.WriteLine("Livro atualizado com sucesso!");
        usuario.ExibirInfo();
        Console.ReadKey();

    }

    public void EmprestarLivro(string codigoLivro, string idUsuario)
    {

    }

    public void DevolverLivro(string codigoLivro)
    {

    }

    public void PesquisarPorTitulo(string titulo)
    {
        var livros = Livros.FindAll(livro => livro.Titulo == titulo);
        ListarLivros(livros);
    }

    public void PesquisarPorAutor(string autor)
    {
        var livros = Livros.FindAll(livro => livro.Autor == autor);
        ListarLivros(livros);
    }

    public void PesquisarPorGenero(string genero)
    {
        var livros = Livros.FindAll(livro => livro.Genero == genero);
        ListarLivros(livros);
    }

    public Livro PesquisarPorIsbn(string isbn)
    {
        var livros = Livros.FindAll(livro => livro.Isbn == isbn);
        return livros[0];
    }

    public int PesquisarUsuarioPorID(int id, bool mostrar=true)
    {
        var usuarios = Usuarios.FindAll(usuario => usuario.Id == id);
        if(mostrar){ListarUsuarios(usuarios);};
        return usuarios[0].Id;
    }

    public string? PesquisarUsuarioPorNome(string nome, bool mostrar=true)
    {
        var usuarios = Usuarios.FindAll(usuario => usuario.Nome == nome);
        if(mostrar){ListarUsuarios(usuarios);};
        return usuarios.Count == 1 ? usuarios[0].Nome : null;
    }


}