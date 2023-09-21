using System.Collections.Generic;
using System;

namespace BasicConnectivity;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int RegionId { get; set; }

    public List<Country> GetAll()
    {
        var countries = new List<Country>();

        using var connection = Provider.GetConnection();
        using var command = Provider.GetCommand();

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
                    countries.Add(new Country
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        RegionId = reader.GetInt32(2)
                    });
                }
                reader.Close();
                connection.Close();

                return countries;
            }
            reader.Close();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        return new List<Country>();
    }
}