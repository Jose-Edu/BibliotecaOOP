public class Menu
{

    public string? op = "";

    public void MainMenu()
    {
        op = MenuOps("SISTEMA DE GERENCIAMENTO DE BIBLIOTECA", ["Livros", "Usuários", "Empréstimos"]);
    }

    public static string? MenuOps(string title, string[] ops) 
    {
        Console.Clear();
        Console.WriteLine($"{title}:");
        Console.WriteLine();
        
        for (int c = 0; c < ops.Length; c++)
        {
            Console.WriteLine($"{c+1} - {ops[c]}");
        };

        Console.WriteLine("Qualquer outro texto - Sair");
        Console.WriteLine();
        Console.Write("Escolha uma opção de gerenciamento: ");
        string? op = Console.ReadLine();
        Console.WriteLine();

        return op;

    }

    public static void Title(string title)
    {
        Console.Clear();
        Console.WriteLine(title+":");
        Console.WriteLine();
    }

}