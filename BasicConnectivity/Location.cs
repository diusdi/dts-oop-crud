using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BasicConnectivity;

public class Location
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Country_id { get; set; }

    private readonly string connectionString =
        "Data Source=DIUS;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    // GET ALL: location
    public List<Location> GetAll()
    {
        var locations = new List<Location>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM locations";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    locations.Add(new location
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
                reader.Close();
                connection.Close();

                return locations;
            }
            reader.Close();
            connection.Close();

            return new List<location>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Location>();
    }

    // GET BY ID: location
    public location GetById(int id)
    {
        var location = new location();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM locations WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    location.Id = reader.GetInt32(0);
                    location.City = reader.GetString(1);
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

        return location;
    }
    // INSERT: location
    public string Insert(string name, int country_id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO locations VALUES (@city, @country_id);";

        try
        {
            command.Parameters.Add(new SqlParameter("@city", city));
            command.Parameters.Add(new SqlParameter("@country_id", country_id));

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

    // UPDATE: location
    public string Update(int id, string city)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE locations SET city = @city WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@city", city);

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

    // DELETE: location
    public string Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM locations WHERE id = @id";
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
