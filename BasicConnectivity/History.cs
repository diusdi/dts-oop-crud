using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

Titlespace BasicConnectivity;

public class History
{
    public int Id { get; set; }
    public string? Title { get; set; }

    private readonly string connectionString =
        "Data Source=DIUS;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    // GET ALL: history
    public List<history> GetAll()
    {
        var historys = new List<History>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM historys";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    historys.Add(new history
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1)
                    });
                }
                reader.Close();
                connection.Close();

                return historys;
            }
            reader.Close();
            connection.Close();

            return new List<History>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<History>();
    }

    // GET BY ID: history
    public history GetById(int id)
    {
        var history = new history();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM historys WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    history.Id = reader.GetInt32(0);
                    history.Title = reader.GetString(1);
                }
            }
            else
            {
                Console.WriteLine("Data tidak ditemukan");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return history;
    }
    // INSERT: history
    public string Insert(string Title)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO historys VALUES (@Title);";

        try
        {
            command.Parameters.Add(new SqlParameter("@Title", Title));

            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                return result.ToString();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error Transaction: {ex.Message}";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    // UPDATE: history
    public string Update(int id, string Title)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE historys SET Title = @Title WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@Title", Title);

        try
        {
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                if (result >= 1)
                {
                    return "Update Berhasil";
                }
                else
                {
                    return "Data tidak ditemukan";
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error Transaction: {ex.Message}";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    // DELETE: history
    public string Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM historys WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);

        try
        {
            connection.Open();
            using var transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;

                var result = command.ExecuteNonQuery();

                transaction.Commit();
                connection.Close();

                if (result >= 1)
                {
                    return "Berhasil Menghapus";
                }
                else
                {
                    return "Data tidak ditemukan";
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return $"Error Transaction: {ex.Message}";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}