using BasicConnectivity.Views;
using System;

namespace BasicConnectivity.Controllers;

public class CountryController
{
    private Country _country;
    private CountryView _countryView;

    public CountryController(Country country, CountryView countryView)
    {
        _country = country;
        _countryView = countryView;
    }

    public void GetAll()
    {
        var results = _country.GetAll();
        if (!results.Any())
        {
            Console.WriteLine("No data found");
        }
        else
        {
            _countryView.List(results, "countries");
        }
    }

    public void Insert()
    {
        var inputCountry = new Country();
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                inputCountry = _countryView.InsertInput();
                if (string.IsNullOrEmpty(inputCountry.Name))
                {
                    Console.WriteLine("country name cannot be empty");
                    continue;
                }
                else if (string.IsNullOrEmpty(inputCountry.Id))
                {
                    Console.WriteLine("country id cannot be empty");
                    continue;
                }
                else if (string.IsNullOrEmpty(Convert.ToString(inputCountry.RegionId)))
                {
                    Console.WriteLine("region id cannot be empty");
                    continue;
                }
                isTrue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        var result = _country.Insert(new Country
        {
            Id = inputCountry.Id, 
            Name = inputCountry.Name,
            RegionId = inputCountry.RegionId,

        });

        _countryView.Transaction(result);
    }

    public void Update()
    {
        var inputCountry = new Country();
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                inputCountry = _countryView.UpdateCountry();
                if (string.IsNullOrEmpty(inputCountry.Name))
                {
                    Console.WriteLine("country name cannot be empty");
                    continue;
                }
                isTrue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        var result = _country.Update(inputCountry);
        _countryView.Transaction(result);
    }
}
