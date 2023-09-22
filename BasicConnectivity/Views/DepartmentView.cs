using System;

namespace BasicConnectivity.Views;

public class DepartmentView : GeneralView
{
    public Department InsertInput()
    {
        Console.WriteLine("Insert id department");
        var id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Insert name department");
        var name = Console.ReadLine();
        Console.WriteLine("Insert location ID");
        var location_id = Convert.ToInt32(Console.ReadLine());

        return new Department
        {
            Id = id,
            Name = name,
            LocationId = location_id
        };
    }

    public Department UpdateDepartment()
    {
        Console.WriteLine("Insert department id");
        var id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Insert name department");
        var name = Console.ReadLine();

        return new Department
        {
            Id = id,
            Name = name
        };
    }
}
