using System.Data;
using System.Data.SQLite;

public static class DataBase
{

    private static SQLiteConnection DBConnect() 
    {
        var connection = new SQLiteConnection("Data Source=database.db; Version=3;");
        connection.Open();
        return connection;
    }

    public static void Query(string query)
    {

        using(var db = DBConnect().CreateCommand())
        {
            db.CommandText = query;
            db.ExecuteNonQuery();
        };
    
    }

    public static DataTable Read(string table, string where)
    {
        var dt = new DataTable();

        using(var db = DBConnect().CreateCommand())
        {
            db.CommandText = $"SELECT * FROM {table} WHERE {where}";
            var dataAdapter = new SQLiteDataAdapter(db.CommandText, DBConnect());
            dataAdapter.Fill(dt);
            return dt;
        }
    }

    public static DataTable Read(string table)
    {
        var dt = new DataTable();

        using(var db = DBConnect().CreateCommand())
        {
            db.CommandText = $"SELECT * FROM {table}";
            var dataAdapter = new SQLiteDataAdapter(db.CommandText, DBConnect());
            dataAdapter.Fill(dt);
            return dt;
        }
    }

    public static void Delete(string table, string where)
    {
        using(var db = DBConnect().CreateCommand())
        {
            db.CommandText = $"DELETE FROM {table} WHERE {where}";
            db.ExecuteNonQuery();
        };
    }

    public static int SetUserId()
    {
        var dt = new DataTable();
        using(var db = DBConnect().CreateCommand())
        {
            db.CommandText = "SELECT id FROM usuarios ORDER BY id DESC; LIMIT 1";
            var dataAdapter = new SQLiteDataAdapter(db.CommandText, DBConnect());
            dataAdapter.Fill(dt);
            return ((int) dt.Rows[0].Field<Int64>("id")) + 1;
        }
    }

}