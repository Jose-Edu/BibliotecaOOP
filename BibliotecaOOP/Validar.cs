public static class Validar
{

    public static string String(string? text)
    {
        text ??= "";
        text = text.Normalize();

        return text;
    }

    public static bool Bool(string? boolString)
    {
        string[] setTrue = {"true", "y", "yes", "s", "sim"};
        boolString = String(boolString);
        boolString = boolString.ToLower();

        return setTrue.Contains(boolString);
    }

    public static string ReadLine(string? title = null)
    {
        Console.Write(String(title));
        string resul = String(Console.ReadLine());
        Console.WriteLine();
        return resul;
    }

    public static string? ReadLineNull(string? title = null)
    {
        Console.Write(String(title));
        string? resul = Console.ReadLine();
        Console.WriteLine();
        return resul;
    }

    public static int ReadLineInt(string? title = null)
    {
        if (title != null)
        {
            Console.Write(title);
        }
        string resul = String(Console.ReadLine());
        Console.WriteLine();
        return Convert.ToInt32(resul);
    }

    public static bool ReadLineBool(string? title = null)
    {
        if (title != null)
        {
            Console.Write(title);
        }
        string resul = String(Console.ReadLine());
        Console.WriteLine();
        return Bool(resul);
    }

}