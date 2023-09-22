using BasicConnectivity.Views;
using System;

namespace BasicConnectivity.Controllers;

public class DepartmentController
{
    private Department _department;
    private DepartmentView _departmentView;

    public DepartmentController(Department department, DepartmentView departmentView)
    {
        _department = department;
        _departmentView = departmentView;
    }

    public void GetAll()
    {
        var results = _department.GetAll();
        if (!results.Any())
        {
            Console.WriteLine("No data found");
        }
        else
        {
            _departmentView.List(results, "Department");
        }
    }

    public void Insert()
    {
        var inputdepartment = new Department();
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                inputdepartment = _departmentView.InsertInput();
                if (string.IsNullOrEmpty(inputdepartment.Name))
                {
                    Console.WriteLine("department name cannot be empty");
                    continue;
                }
                else if (string.IsNullOrEmpty(Convert.ToString(inputdepartment.LocationId)))
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

        var result = _department.Insert(new Department
        {
            Id = inputdepartment.Id,
            Name = inputdepartment.Name,
            LocationId = inputdepartment.LocationId,

        });

        _departmentView.Transaction(result);
    }

    public void Update()
    {
        var inputdepartment = new Department();
        var isTrue = true;
        while (isTrue)
        {
            try
            {
                inputdepartment = _departmentView.UpdateDepartment();
                if (string.IsNullOrEmpty(inputdepartment.Name))
                {
                    Console.WriteLine("department name cannot be empty");
                    continue;
                }
                isTrue = false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        var result = _department.Update(inputdepartment);
        _departmentView.Transaction(result);
    }
}
