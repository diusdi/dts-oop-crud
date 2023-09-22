using System.Collections.Generic;
using System;

namespace BasicConnectivity;

public class Location
{
    public int Id { get; set; }
    public string City { get; set; }
    public string CountryId { get; set; }

    public override string ToString()
    {
        return $"{Id} - {City} - {CountryId}";
    }

    public List<Location> GetAll()
    {
        var locations = new List<Location>();

        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

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
                    locations.Add(new Location
                    {
                        Id = reader.GetInt32(0),
                        City = reader.GetString(3),
                        CountryId = reader.GetString(5)
                    });
                }
                reader.Close();
                connection.Close();

                return locations;
            }
            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Location>();
    }

    public string Insert(Location location)
    {
        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "INSERT INTO locations(id, city, country_id) VALUES (@id, @city, @country_id);";

        try
        {
            command.Parameters.Add(Provider.SetParameter("@id", location.Id));
            command.Parameters.Add(Provider.SetParameter("@city", location.City));
            command.Parameters.Add(Provider.SetParameter("@country_id", location.CountryId));

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

    public string Update(Location location)
    {
        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

        command.Connection = connection;
        command.CommandText = "UPDATE locations SET city=@city where id = @id";
        command.Parameters.Add(Provider.SetParameter("@city", location.City));
        command.Parameters.Add(Provider.SetParameter("@id", location.Id));

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