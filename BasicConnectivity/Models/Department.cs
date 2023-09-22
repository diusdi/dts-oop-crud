using System.Collections.Generic;
using System;

namespace BasicConnectivity;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LocationId { get; set; }

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
}