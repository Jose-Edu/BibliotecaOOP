var biblioteca = new Biblioteca();
var menu = new Menu();

while (true)
{
    menu.MainMenu();

    switch(menu.op)
    {
        case "1":

            menu.op = Menu.MenuOps("Livros", ["Cadastrar novo livro", "Consultar livro", "Editar livro", "Listar Livros"]);

            switch(menu.op)
            {
                case "1":
                    Menu.Title("Cadastrar novo livro");
                    biblioteca.CadastrarLivro(new Livro(
                        Validar.ReadLine("Digite o título do livro: "), 
                        Validar.ReadLine("Digite o código ISBN do livro: "), 
                        Validar.ReadLine("Digite o autor do livro: "), 
                        Validar.ReadLine("Digite o gênero do livro: "), 
                        Validar.ReadLineInt("Digite a quantidade de exemplares disponíveis")
                        ));
                    break;
                
                case "2":
                    menu.op = Menu.MenuOps("Consultar livro", ["Consultar por título", "Consultar por autor", "Consultar por gênero"]);
                    
                    switch(menu.op)
                    {
                        case "1":
                            biblioteca.PesquisarPorTitulo(Validar.ReadLine("Digite o título do livro: "));
                            break;
                        case "2":
                            biblioteca.PesquisarPorAutor(Validar.ReadLine("Digite o autor desejado: "));
                            break;
                        case "3":
                            biblioteca.PesquisarPorGenero(Validar.ReadLine("Digite o gênero desejado: "));
                            break;
                        default:
                            break;
                    }

                    break;

                case "3":
                    Menu.Title("Editar livro");
                    Livro livro = biblioteca.PesquisarPorIsbn(Validar.ReadLine("Digite o ISBN do livro: "));
                    livro = biblioteca.Livros[biblioteca.Livros.IndexOf(livro)];
                    biblioteca.EditarLivro(livro, [
                        Validar.ReadLine($"Digite o novo titulo do livro ou enter para manter {livro.Titulo}: "),
                        Validar.ReadLine($"Digite o novo autor do livro ou enter para manter {livro.Autor}: "),
                        Validar.ReadLine($"Digite o novo gênero do livro ou enter para manter {livro.Genero}: "),
                        Validar.ReadLine($"Digite a nova quantidade em estoque ou enter para manter {livro.Quantidade}: ")
                    ]);
                    break;

                case "4":
                    biblioteca.ListarLivros();
                    break;

                default:
                    break;
            }

            break;

        case "2":

            menu.op = Menu.MenuOps("Usuários", ["Cadastrar usuário", "Consultar usuário", "Editar usuário", "Listar usuários"]);

            switch(menu.op)
            {
                case "1":
                    Menu.Title("Cadastrar usuário");
                    biblioteca.CadastrarUsuario(new Usuario(
                        Validar.ReadLine("Digite o nome do usuário: "), 
                        Validar.ReadLine("Digite o contato do usuário (email ou telefone): "), 
                        Validar.ReadLine("Digite o endereço do usuário: ")
                        ));
                    break;
                
                case "2":
                    menu.op = Menu.MenuOps("Consultar usuário:", ["Consultar por ID", "Consultar por nome"]);

                    switch(menu.op)
                    {
                        case "1":
                            biblioteca.PesquisarUsuarioPorID(Validar.ReadLineInt("Digite o ID do usuário: "));
                            break;

                        case "2":
                            biblioteca.PesquisarUsuarioPorNome(Validar.ReadLine("Digite o nome do usuário: "));
                            break;
                        
                        default:
                            break;
                    };
                    break;

                case "3":
                    Menu.Title("Editar usuário");
                    Usuario user = biblioteca.PesquisarUsuarioPorID(Validar.ReadLineInt("Digite o ID do usuário: "), false);
                    user = biblioteca.Usuarios[biblioteca.Usuarios.IndexOf(user)];
                    biblioteca.EditarUsuario(user, [
                        Validar.ReadLine($"Digite o novo nome do usuário ou enter para manter {user.Nome}: "),
                        Validar.ReadLine($"Digite o novo contato do usuário ou enter para manter {user.Contato}: "),
                        Validar.ReadLine($"Digite o novo endereco do usuário ou enter para manter {user.Endereco}: ")
                    ]);
                    break;

                case "4":
                    biblioteca.ListarUsuarios();
                    break;

                default:
                    break;
            }

            break;

        case "3":
            menu.op = Menu.MenuOps("Empréstimo", ["Registrar empréstimo", "Registrar devolução", "Listar empréstimos", "Listar empréstimos por usuário", "Listar empréstimos por livro"]);

            switch(menu.op)
            {
                case "1":
                    Menu.Title("Emprestar livro");
                    biblioteca.EmprestarLivro(
                        Validar.ReadLine("Digite o código ISBN do livro: "),
                        Validar.ReadLineInt("Digite o id do usuário para emprestar: "),
                        Validar.ReadLineInt("Digite o número de dias de emprestimo: ")
                    );
                    break;
                
                case "2":
                    Menu.Title("Devolver livro");
                    biblioteca.DevolverLivro(
                        Validar.ReadLine("Digite o código ISBN do livro: "),
                        Validar.ReadLineInt("Digite o ID do usuário: ")
                    );
                    break;

                case "3":
                    Menu.Title("Listar empréstimos");
                    biblioteca.ListarEmprestimos(Validar.ReadLineBool("Mostrar empréstimos fechados? [s/n]: "));
                    break;

                case "4":
                    Menu.Title("Listar emprestimos por usuário");
                    biblioteca.PesquisarEmprestimoPorUsuario(
                        Validar.ReadLineInt("Digite o ID do usuário: "),
                        Validar.ReadLineBool("Mostrar empréstimos fechados? [s/n]: ")
                    );
                    break;
                
                case "5":
                    Menu.Title("Listar emprestimos por livro");
                    biblioteca.PesquisarEmprestimoPorLivro(
                        Validar.ReadLine("Digite o código ISBN do livro: "),
                        Validar.ReadLineBool("Mostrar empréstimos fechados? [s/n]: ")
                    );
                    break;

                default:
                    break;
            }

            break;
        
        default:
            Console.WriteLine("Saindo do sistema...");
            Environment.Exit(0);
            break;
    }

}