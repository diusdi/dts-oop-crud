using BasicConnectivity.Views;
using System;

namespace BasicConnectivity.Controllers;

public class LocationController
{
    private Location _location;
    private LocationView _locationView;

    public LocationController(Location location, LocationView locationView)
    {
        _location = location;
        _locationView = locationView;
    }

    public void GetAll()
    {
        var results = _location.GetAll();
        if (!results.Any())
        {
            Console.WriteLine("No data found");
        }
        else
        {
            _locationView.List(results, "countries");
        }
    }

    public void Insert()
    {
        var inputLocation = new Location();
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                inputLocation = _locationView.InsertInput();
                if (string.IsNullOrEmpty(inputLocation.City))
                {
                    Console.WriteLine("location name cannot be empty");
                    continue;
                }
                else if (string.IsNullOrEmpty(inputLocation.CountryId))
                {
                    Console.WriteLine("country id cannot be empty");
                    continue;
                }
                isTrue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        var result = _location.Insert(new Location
        {
            Id = inputLocation.Id,
            City = inputLocation.City,
            CountryId = inputLocation.CountryId,

        });

        _locationView.Transaction(result);
    }

    public void Update()
    {
        var inputLocation = new Location();
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                inputLocation = _locationView.UpdateLocation();
                if (string.IsNullOrEmpty(inputLocation.City))
                {
                    Console.WriteLine("location name cannot be empty");
                    continue;
                }
                isTrue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        var result = _location.Update(inputLocation);
        _locationView.Transaction(result);
    }
}
