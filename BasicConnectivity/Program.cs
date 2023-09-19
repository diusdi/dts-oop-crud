using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace BasicConnectivity;

public class Program
{
    private static readonly string connectionString = "Data Source=DIUS;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    private static void Main()
    {
        // Menampilkan seluruh region
        //GetAllRegions();

        // Menambahkan data region baru
        //InsertRegion("Jawa Utara");

        // Menampilkan data region berdasarkan id
        //GetRegionById(11);

        // Mengubah data region berdasarkan id
        //UpdateRegion(110, "Jawa Selatan");

        // Menghapus data region berdasarkan id
        //DeleteRegion(11);
    }

    // MENAMPILKAN SEMUA DATA REGION
    public static void GetAllRegions()
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        // QUERY MENAMPILKAN SELURUH DATA REGION
        command.CommandText = "SELECT * FROM regions";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                }
            else
                Console.WriteLine("No rows found.");

            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // MENAMPILKAN DATA REGION BERDASARKAN ID
    public static void GetRegionById(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        // QUERY MENAMPILKAN DATA REGION BERDASARKAN ID
        command.CommandText = "SELECT * FROM regions where id = @id";

        try
        {
            connection.Open();
            var pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.Value = id;
            pId.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(pId);


            using var reader = command.ExecuteReader();

            if (reader.HasRows)
                while (reader.Read())
                {
                    Console.WriteLine("Id: " + reader.GetInt32(0));
                    Console.WriteLine("Name: " + reader.GetString(1));
                }
            else
                Console.WriteLine("Data tidak ditemukan");

            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // MENAMBAHKAN DATA REGION
    public static void InsertRegion(string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        // QUERY MENAMBAHKAN DATA REGION
        command.CommandText = "INSERT INTO regions VALUES (@name);";

        try
        {
            var pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.Value = name;
            pName.SqlDbType = SqlDbType.VarChar;
            command.Parameters.Add(pName);

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                switch (result)
                {
                    case >= 1:
                        Console.WriteLine("Insert Berhasil");
                        break;
                    default:
                        Console.WriteLine("Insert Gagal");
                        break;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error Transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // MENGUBAH DATA REGION BERDASARKAN ID
    public static void UpdateRegion(int id, string name)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        // QUERY MENGUBAH DATA REGION
        command.CommandText = "Update regions SET name = @name WHERE id=@id;";

        try
        {
            var pName = new SqlParameter();
            pName.ParameterName = "@name";
            pName.Value = name;
            pName.SqlDbType = SqlDbType.VarChar;
            var pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.Value = id;
            pId.SqlDbType = SqlDbType.VarChar;
            command.Parameters.Add(pId);
            command.Parameters.Add(pName);

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                switch (result)
                {
                    case >= 1:
                        Console.WriteLine("Update Berhasil");
                        break;
                    default:
                        Console.WriteLine("Data tidak ditemukan");
                        break;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error Transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // MENGHAPUS DATA REGION
    public static void DeleteRegion(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        // QUERY MENGHAPUS DATA REGION
        command.CommandText = "DELETE regions WHERE id=@id;";

        try
        {
            var pId = new SqlParameter();
            pId.ParameterName = "@id";
            pId.Value = id;
            pId.SqlDbType = SqlDbType.VarChar;
            command.Parameters.Add(pId);

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                switch (result)
                {
                    case >= 1:
                        Console.WriteLine("Berhasil Menghapus");
                        break;
                    default:
                        Console.WriteLine("Data tidak ditemukan");
                        break;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error Transaction: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
