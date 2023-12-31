﻿using BasicConnectivity.Controllers;
using BasicConnectivity.Views;

namespace BasicConnectivity;

public class Program
{
    private static void Main()
    {
        var choice = true;
        while (choice)
        {
            Console.WriteLine("1. Region CRUD");
            Console.WriteLine("2. Country CRUD");
            Console.WriteLine("3. Locations CRUD");
            Console.WriteLine("4. Departments CRUD");
            Console.WriteLine("5. Employee CRUD");
            Console.WriteLine("10. Exit");
            Console.Write("Enter your choice: ");
            var input = Console.ReadLine();
            choice = Menu(input);
        }
    }

    public static bool Menu(string input)
    {
        switch (input)
        {
            case "1":
                RegionMenu();
                break;
            case "2":
                CountryMenu();
                break;
            case "3":
                LocationMenu();
                break;
            case "4":
                DepartmentMenu();
                break;
            case "5":
                EmployeeMenu();
                break;
            case "10":
                return false;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }

        return true;
    }

    public static void RegionMenu()
    {
        var region = new Region();
        var regionView = new RegionView();

        var regionController = new RegionController(region, regionView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all regions");
            Console.WriteLine("2. Insert new region");
            Console.WriteLine("3. Update region");
            Console.WriteLine("4. Delete region");
            Console.WriteLine("10. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    regionController.GetAll();
                    break;
                case "2":
                    regionController.Insert();
                    break;
                case "3":
                    regionController.Update();
                    break;
                case "10":
                    isLoop = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    public static void CountryMenu()
    {
        var country = new Country();
        var countryView = new CountryView();

        var countryController = new CountryController(country, countryView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all countries");
            Console.WriteLine("2. Insert new country");
            Console.WriteLine("3. Update country");
            Console.WriteLine("4. Delete country");
            Console.WriteLine("10. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    countryController.GetAll();
                    break;
                case "2":
                    countryController.Insert();
                    break;
                case "3":
                    countryController.Update();
                    break;
                case "10":
                    isLoop = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    public static void LocationMenu()
    {
        var location = new Location();
        var locationView = new LocationView();

        var locationController = new LocationController(location, locationView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all locations");
            Console.WriteLine("2. Insert new location");
            Console.WriteLine("3. Update location");
            Console.WriteLine("4. Delete location");
            Console.WriteLine("10. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    locationController.GetAll();
                    break;
                case "2":
                    locationController.Insert();
                    break;
                case "3":
                    locationController.Update();
                    break;
                case "10":
                    isLoop = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    public static void DepartmentMenu()
    {
        var department = new Department();
        var departmentView = new DepartmentView();

        var departmentController = new DepartmentController(department, departmentView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all departments");
            Console.WriteLine("2. Insert new department");
            Console.WriteLine("3. Update department");
            Console.WriteLine("4. Delete department");
            Console.WriteLine("10. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    departmentController.GetAll();
                    break;
                case "2":
                    departmentController.Insert();
                    break;
                case "3":
                    departmentController.Update();
                    break;
                case "10":
                    isLoop = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }

    public static void EmployeeMenu()
    {
        var employee = new Employee();
        var employeeView = new EmployeeView();

        var employeeController = new EmployeeController(employee, employeeView);

        var isLoop = true;
        while (isLoop)
        {
            Console.WriteLine("1. List all employees");
            Console.WriteLine("2. Insert new employee");
            Console.WriteLine("3. Update employee");
            Console.WriteLine("4. Delete employee");
            Console.WriteLine("10. Back");
            Console.Write("Enter your choice: ");
            var input2 = Console.ReadLine();
            switch (input2)
            {
                case "1":
                    employeeController.GetAll();
                    break;
                case "2":
                    employeeController.Insert();
                    break;
                case "3":
                    employeeController.Update();
                    break;
                case "10":
                    isLoop = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}
