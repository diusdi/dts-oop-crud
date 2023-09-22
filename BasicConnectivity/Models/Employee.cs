using System.Collections.Generic;
using System;

namespace BasicConnectivity;

public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Salary { get; set; }
    public int DepartmentId { get; set; }

    public List<Employee> GetAll()
    {
        var employees = new List<Employee>();

        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

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
                    employees.Add(new Employee
                    {
                        Id = reader.GetInt32(0),
                        FullName = reader.GetString(1),
                        Email = reader.GetString(3),
                        PhoneNumber = reader.GetString(4),
                        Salary = reader.GetInt32(6),
                        DepartmentId = reader.GetInt32(10)
                    }) ;
                }
                reader.Close();
                connection.Close();

                return employees;
            }
            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Employee>();
    }
}