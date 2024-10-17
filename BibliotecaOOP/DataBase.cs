using System.Data.SQLite;

public static class DataBase
{

    private static SQLiteConnection DBConnect() 
    {
        var connection = new SQLiteConnection("Data Source=database.db; Version=3;");
        connection.Open();
        return connection;
    }

    private static void Cadastrar()
    {
        
    }

    private static void Consultar()
    {

    }

    private static void Atualizar()
    {

    }

}