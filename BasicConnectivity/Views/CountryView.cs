using System;

namespace BasicConnectivity.Views;

public class CountryView : GeneralView
{
    public Country InsertInput()
    {
        Console.WriteLine("Insert country name");
        var name = Console.ReadLine();
        Console.WriteLine("Insert country ID");
        var country_id = Console.ReadLine();
        Console.WriteLine("Insert region id");
        var region_id = Convert.ToInt32(Console.ReadLine());

        return new Country
        {
            RegionId = region_id,
            Id = country_id,
            Name = name
        };
    }

    public Country UpdateCountry()
    {
        Console.WriteLine("Insert country id");
        var id = Console.ReadLine();
        Console.WriteLine("Insert country name");
        var name = Console.ReadLine();

        return new Country
        {
            Id = id,
            Name = name
        };
    }
}
