using System;

namespace BasicConnectivity.Views;

public class LocationView : GeneralView
{
    public Location InsertInput()
    {
        Console.WriteLine("Insert id location");
        var id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Insert street address");
        var city = Console.ReadLine();
        Console.WriteLine("Insert country ID");
        var country_id = Console.ReadLine();

        return new Location
        {
            Id = id,
            City = city,
            CountryId = country_id
        };
    }

    public Location UpdateLocation()
    {
        Console.WriteLine("Insert location id");
        var id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Insert street address");
        var city = Console.ReadLine();

        return new Location
        {
            Id = id,
            City = city
        };
    }
}
