Menu menu = new Menu();

while (true)
{
    menu.MainMenu();

    switch(menu.op)
    {
        case "1":

            menu.op = Menu.MenuOps("Livros", ["Cadastrar livros", "Consultar livros", "Atualizar livros"]);

            switch(menu.op)
            {
                case "1":
                    Console.WriteLine("Cadastrar livro");
                    break;
                
                case "2":
                    Console.WriteLine("Consultar livro");
                    break;

                case "3":
                    Console.WriteLine("Atualizar livro");
                    break;

                default:
                    break;
            }

            break;

        case "2":

            menu.op = Menu.MenuOps("Usuários", ["Cadastrar usuário", "Consultar usuário", "Atualizar usuário"]);

            switch(menu.op)
            {
                case "1":
                    Console.WriteLine("Cadastrar usuário");
                    break;
                
                case "2":
                    Console.WriteLine("Consultar usuário");
                    break;

                case "3":
                    Console.WriteLine("Atualizar usuário");
                    break;

                default:
                    break;
            }

            break;

        case "3":
            menu.op = Menu.MenuOps("Empréstimo", ["Registrar empréstimo", "Registrar devolução", "Histórico de empréstimos"]);

            switch(menu.op)
            {
                case "1":
                    Console.WriteLine("Registrar empréstimo");
                    break;
                
                case "2":
                    Console.WriteLine("Registrar devolução");
                    break;

                case "3":
                    Console.WriteLine("Histórico de empréstimos");
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