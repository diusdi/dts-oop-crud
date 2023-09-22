using System.Collections.Generic;
using System;

namespace BasicConnectivity;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LocationId { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Name} - {LocationId}";
    }

    public List<Department> GetAll()
    {
        var departments = new List<Department>();

        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "SELECT * FROM departments";

        try
        {
            connection.Open();

            using var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    departments.Add(new Department
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        LocationId = reader.GetInt32(2)
                    });
                }
                reader.Close();
                connection.Close();

                return departments;
            }
            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Department>();
    }

    public string Insert(Department department)
    {
        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO departments(id, name, location_id) VALUES (@id, @name, @location_id);";

        try
        {
            command.Parameters.Add(Provider.SetParameter("@id", department.Id));
            command.Parameters.Add(Provider.SetParameter("@name", department.Name));
            command.Parameters.Add(Provider.SetParameter("@location_id", department.LocationId));

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

    public string Update(Department department)
    {
        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE departments SET name=@name where id = @id";
        command.Parameters.Add(Provider.SetParameter("@name", department.Name));
        command.Parameters.Add(Provider.SetParameter("@id", department.Id));

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

    public string Delete(int id)
    {
        return "";
    }
}