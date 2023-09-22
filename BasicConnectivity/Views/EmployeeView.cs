using System;

namespace BasicConnectivity.Views;

public class EmployeeView : GeneralView
{
    public Employee InsertInput()
    {
        Console.WriteLine("Insert id Employee");
        var id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Insert first name");
        var first_name = Console.ReadLine();
        Console.WriteLine("Insert last name");
        var last_name = Console.ReadLine();
        Console.WriteLine("Insert email");
        var email = Console.ReadLine();
        Console.WriteLine("Insert job id");
        var job_id = Console.ReadLine();
        Console.WriteLine("Insert department id");
        var department_id = Convert.ToInt32(Console.ReadLine());

        return new Employee
        {
            Id = id,
            FirstName = first_name,
            LastName = last_name,
            Email = email,
            JobId =job_id, 
            DepartmentId = department_id
        };
    }

    public Employee UpdateEmployee()
    {
        Console.WriteLine("Insert employee id");
        var id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Insert first name");
        var name = Console.ReadLine();

        return new Employee
        {
            Id = id,
            FirstName = name
        };
    }
}
