using BasicConnectivity.Views;
using System;

namespace BasicConnectivity.Controllers;

public class EmployeeController
{
    private Employee _employee;
    private EmployeeView _employeeView;

    public EmployeeController(Employee employee, EmployeeView employeeView)
    {
        _employee = employee;
        _employeeView = employeeView;
    }

    public void GetAll()
    {
        var results = _employee.GetAll();
        if (!results.Any())
        {
            Console.WriteLine("No data found");
        }
        else
        {
            _employeeView.List(results, "Employee");
        }
    }

    public void Insert()
    {
        var inputEmployee = new Employee();
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                inputEmployee = _employeeView.InsertInput();
                if (string.IsNullOrEmpty(inputEmployee.FirstName))
                {
                    Console.WriteLine("first name cannot be empty");
                    continue;
                }
                else if (string.IsNullOrEmpty(inputEmployee.LastName))
                {
                    Console.WriteLine("last name cannot be empty");
                    continue;
                }
                else if (string.IsNullOrEmpty(inputEmployee.Email))
                {
                    Console.WriteLine("email cannot be empty");
                    continue;
                }
                else if (string.IsNullOrEmpty(inputEmployee.JobId))
                {
                    Console.WriteLine("job id cannot be empty");
                    continue;
                }
                else if (string.IsNullOrEmpty(Convert.ToString(inputEmployee.DepartmentId)))
                {
                    Console.WriteLine("department id cannot be empty");
                    continue;
                }
                isTrue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        var result = _employee.Insert(new Employee
        {
            Id = inputEmployee.Id,
            FirstName = inputEmployee.FirstName,
            LastName = inputEmployee.LastName,
            Email = inputEmployee.Email,
            JobId = inputEmployee.JobId, 
            DepartmentId = inputEmployee.DepartmentId

        });

        _employeeView.Transaction(result);
    }

    public void Update()
    {
        var inputEmployee = new Employee();
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                inputEmployee = _employeeView.UpdateEmployee();
                if (string.IsNullOrEmpty(inputEmployee.FirstName))
                {
                    Console.WriteLine("employee name cannot be empty");
                    continue;
                }
                isTrue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        var result = _employee.Update(inputEmployee);
        _employeeView.Transaction(result);
    }
}
