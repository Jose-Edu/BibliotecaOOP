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
                    Console.Clear();
                    Console.WriteLine("Cadastrar livro:");
                    Console.WriteLine();
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
                    var livro = biblioteca.Livros[biblioteca.PesquisarPorIsbn(Validar.ReadLine("Digite o ISBN do livro: "))];
                    biblioteca.EditarUsuario(usuario, [
                        Validar.ReadLineNull($"Digite o novo nome do usuário ou enter para manter {usuario.Nome}: "),
                        Validar.ReadLineNull($"Digite o novo contato do usuário ou enter para manter {usuario.Contato}: "),
                        Validar.ReadLineNull($"Digite o novo endereco do usuário ou enter para manter {usuario.Endereco}: ")
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
                    Console.Clear();
                    Console.WriteLine("Cadastrar Usuário:");
                    Console.WriteLine();
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
                    int id = biblioteca.PesquisarUsuarioPorID(Validar.ReadLineInt("Digite o ID do usuário: "), false);
                    var usuario = biblioteca.Usuarios[];
                    biblioteca.EditarUsuario(usuario, [
                        Validar.ReadLineNull($"Digite o novo nome do usuário ou enter para manter {usuario.Nome}: "),
                        Validar.ReadLineNull($"Digite o novo contato do usuário ou enter para manter {usuario.Contato}: "),
                        Validar.ReadLineNull($"Digite o novo endereco do usuário ou enter para manter {usuario.Endereco}: ")
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
            menu.op = Menu.MenuOps("Empréstimo", ["Registrar empréstimo", "Registrar devolução", "Listar empréstimos"]);

            switch(menu.op)
            {
                case "1":
                    //biblioteca.EmprestarLivro();
                    break;
                
                case "2":
                    //biblioteca.DevolverLivro();
                    break;

                case "3":
                    biblioteca.ListarEmprestimos();
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