using System.Collections.Generic;
using System;

namespace BasicConnectivity;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string JobId { get; set; }
    public int DepartmentId { get; set; }

    public override string ToString()
    {
        return $"ID : {Id}\n" +
               $"Nama : {FirstName} {LastName}\n" +
               $"Email : {Email}\n" +
               $"JobId : {JobId}\n" +
               $"DepartmentId : {DepartmentId}\n" +
               $"-----------------\n";
    }

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
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3),
                        JobId = reader.GetString(9),
                        DepartmentId = reader.GetInt32(10)
                    });
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

    public string Insert(Employee employee)
    {
        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO employees(id, first_name, last_name, email, hire_date, job_id, department_id) VALUES (@id, @first_name, @last_name, @email, @hire_date, @job_id, @department_id);";

        try
        {
            command.Parameters.Add(Provider.SetParameter("@id", employee.Id));
            command.Parameters.Add(Provider.SetParameter("@first_name", employee.FirstName));
            command.Parameters.Add(Provider.SetParameter("@last_name", employee.LastName));
            command.Parameters.Add(Provider.SetParameter("@email", employee.Email));
            command.Parameters.Add(Provider.SetParameter("@hire_date", DateTime.Now));
            command.Parameters.Add(Provider.SetParameter("@job_id", employee.JobId));
            command.Parameters.Add(Provider.SetParameter("@department_id", employee.DepartmentId));

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

    public string Update(Employee employee)
    {
        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE employees SET first_name=@name where id = @id";
        command.Parameters.Add(Provider.SetParameter("@name", employee.FirstName));
        command.Parameters.Add(Provider.SetParameter("@id", employee.Id));

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