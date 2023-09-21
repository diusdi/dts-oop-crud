using System.Collections.Generic;
using System;

namespace BasicConnectivity;

public class Location
{
    public int Id { get; set; }
    public string StreetAddress { get; set; }
    public int CountryId { get; set; }

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
                        StreetAddress = reader.GetString(1),
                        CountryId = reader.GetInt32(5)
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
}