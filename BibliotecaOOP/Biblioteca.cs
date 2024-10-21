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
        DataTable emprestimos = incluirFechados ? DataBase.Read("emprestimos") : DataBase.Read("emprestimos", "fechamento='Pendente'");

        Menu.Title("Emprestimos encontrados");
        foreach(DataRow row in emprestimos.Rows)
        {
            Livro livro = PesquisarPorIsbn(Validar.String(row.Field<string>("book_isbn")));
            livro = Livros[Livros.IndexOf(livro)];
            Usuario usuario = PesquisarUsuarioPorID((int) row.Field<Int64>("user_id"), false);
            usuario = Usuarios[Usuarios.IndexOf(usuario)];

            Console.WriteLine($"| Livro: {livro.Titulo}({livro.Isbn}) / Usuário: {usuario.Nome}({usuario.Id}) / Data Limite: {row.Field<string>("limite")} / Fechamento: {row.Field<string>("fechamento")} |");
        }
        Console.ReadKey();
        Console.WriteLine();
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

    public void EditarLivro(Livro livro, string[] campos)
    {

        livro.Titulo = campos[0] != "" ? campos[0] : livro.Titulo;
        livro.Autor = campos[1] != "" ? campos[1] : livro.Autor;
        livro.Genero = campos[2] != "" ? campos[2] : livro.Genero;
        livro.Quantidade = campos[3] != "" ? Convert.ToInt32(campos[3]): livro.Quantidade;

        DataBase.Query($"UPDATE livros SET titulo='{livro.Titulo}', autor='{livro.Autor}', genero='{livro.Genero}', quantidade={livro.Quantidade} WHERE isbn='{livro.Isbn}' LIMIT 1");

        Console.WriteLine("Livro atualizado com sucesso!");
        livro.ExibirInfo();
        Console.ReadKey();
        
    }

    public void EditarUsuario(Usuario usuario, string[] campos)
    {

        usuario.Nome = campos[0] != "" ? campos[0] : usuario.Nome;
        usuario.Contato = campos[1] != "" ? campos[1] : usuario.Contato;
        usuario.Endereco = campos[2] != "" ? campos[2] : usuario.Endereco;

        DataBase.Query($"UPDATE usuarios SET nome = '{usuario.Nome}', contato='{usuario.Contato}', endereco='{usuario.Endereco}' WHERE id={usuario.Id} LIMIT 1");

        Console.WriteLine("Usuário atualizado com sucesso!");
        usuario.ExibirInfo();
        Console.ReadKey();

    }

    public void EmprestarLivro(string codigoLivro, int idUsuario, int dias)
    {
        Livro livro = PesquisarPorIsbn(codigoLivro);
        livro = Livros[Livros.IndexOf(livro)];
        Usuario usuario = PesquisarUsuarioPorID(idUsuario, false);
        usuario = Usuarios[Usuarios.IndexOf(usuario)];

        livro.Emprestimo(usuario, dias);
    }

    public void DevolverLivro(string codigoLivro, int idUsuario)
    {
        Livro livro = PesquisarPorIsbn(codigoLivro);
        livro = Livros[Livros.IndexOf(livro)];
        Usuario usuario = PesquisarUsuarioPorID(idUsuario, false);
        usuario = Usuarios[Usuarios.IndexOf(usuario)];

        livro.Devolucao(usuario);
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

    public Usuario PesquisarUsuarioPorID(int id, bool mostrar=true)
    {
        var usuarios = Usuarios.FindAll(usuario => usuario.Id == id);
        if(mostrar){ListarUsuarios(usuarios);};
        return usuarios[0];
    }

    public Usuario? PesquisarUsuarioPorNome(string nome, bool mostrar=true)
    {
        var usuarios = Usuarios.FindAll(usuario => usuario.Nome == nome);
        if(mostrar){ListarUsuarios(usuarios);};
        return usuarios.Count == 1 ? usuarios[0] : null;
    }

    public void PesquisarEmprestimoPorUsuario(int id, bool incluirFechados=false)
    {
        Usuario usuario = PesquisarUsuarioPorID(id, false);
        usuario = Usuarios[Usuarios.IndexOf(usuario)];

        DataTable emprestimos = incluirFechados ? DataBase.Read("emprestimos", $"user_id={usuario.Id}") : DataBase.Read("emprestimos", $"fechamento='Pendente' AND user_id={usuario.Id}");

        Menu.Title($"Emprestimos encontrados para o usuario {usuario.Nome}({usuario.Id})");
        foreach(DataRow row in emprestimos.Rows)
        {
            Livro livro = PesquisarPorIsbn(Validar.String(row.Field<string>("book_isbn")));
            livro = Livros[Livros.IndexOf(livro)];

            Console.WriteLine($"| Livro: {livro.Titulo}({livro.Isbn}) / Data Limite: {row.Field<string>("limite")} / Fechamento: {row.Field<string>("fechamento")} |");

        }
        Console.ReadKey();
        Console.WriteLine();
    }

    public void PesquisarEmprestimoPorLivro(string isbn, bool incluirFechados=false)
    {
        Livro livro = PesquisarPorIsbn(isbn);
        livro = Livros[Livros.IndexOf(livro)];
        
        DataTable emprestimos = incluirFechados ? DataBase.Read("emprestimos", $"book_isbn={livro.Isbn}") : DataBase.Read("emprestimos", $"fechamento='Pendente' AND book_isbn={livro.Isbn}");

        Menu.Title($"Emprestimos encontrados para o livro {livro.Titulo}({livro.Isbn})");
        foreach(DataRow row in emprestimos.Rows)
        {
            Usuario usuario = PesquisarUsuarioPorID((int) row.Field<Int64>("user_id"), false);
            usuario = Usuarios[Usuarios.IndexOf(usuario)];

            Console.WriteLine($"| Usuário: {usuario.Nome}({usuario.Id}) / Data Limite: {row.Field<string>("limite")} / Fechamento: {row.Field<string>("fechamento")} |");

        }
        Console.ReadKey();
        Console.WriteLine();
    }
}