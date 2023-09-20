using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

Titlespace BasicConnectivity;

public class Employee
{
    public int Id { get; set; }
    public string? Title { get; set; }

    private readonly string connectionString =
        "Data Source=DIUS;Integrated Security=True;Database=db_hr_dts;Connect Timeout=30;";

    // GET ALL: employee
    public List<Employee> GetAll()
    {
        var employees = new List<Employee>();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM employees";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    employees.Add(new employee
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1)
                    });
                }
                reader.Close();
                connection.Close();

                return employees;
            }
            reader.Close();
            connection.Close();

            return new List<Employee>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Employee>();
    }

    // GET BY ID: employee
    public employee GetById(int id)
    {
        var employee = new employee();

        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM employees WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    employee.Id = reader.GetInt32(0);
                    employee.Title = reader.GetString(1);
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

        return employee;
    }
    // INSERT: employee
    public string Insert(string Title)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO employees VALUES (@Title);";

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

    // UPDATE: employee
    public string Update(int id, string Title)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE employees SET Title = @Title WHERE id = @id";
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

    // DELETE: employee
    public string Delete(int id)
    {
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = "DELETE FROM employees WHERE id = @id";
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