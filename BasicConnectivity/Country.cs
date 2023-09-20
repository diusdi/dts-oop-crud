using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

Titlespace BasicConnectivity;

public class Country
{
    public int Id { get; set; }
    public string? Title { get; set; }

    private readonly string connectionString =
        "Data Source=DIUS;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    // GET ALL: country
    public List<Country> GetAll()
    {
        var countrys = new List<Country>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM countries";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countrys.Add(new country
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1)
                    });
                }
                reader.Close();
                connection.Close();

                return countrys;
            }
            reader.Close();
            connection.Close();

            return new List<Country>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Country>();
    }

    // GET BY ID: country
    public country GetById(int id)
    {
        var country = new country();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM countries WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    country.Id = reader.GetInt32(0);
                    country.Title = reader.GetString(1);
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

        return country;
    }
    // INSERT: country
    public string Insert(string Title)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO countries VALUES (@Title);";

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

    // UPDATE: country
    public string Update(int id, string Title)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE countries SET Title = @Title WHERE id = @id";
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

    // DELETE: country
    public string Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM countries WHERE id = @id";
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