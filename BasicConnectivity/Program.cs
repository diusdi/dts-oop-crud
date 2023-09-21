using System.Data;
using System.Data.SqlClient;

namespace BasicConnectivity;

public class Program
{
    private static void Main()
    {
        var choice = true;
        while (choice)
        {
            Console.WriteLine("1. List Data Employee");
            Console.WriteLine("2. List Data Departments");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");
            var input = Console.ReadLine();
            choice = Menu(input);
        }
    }
    public static bool Menu(string? input)
    {
        var employee = new Employee();
        var department = new Department();
        var location = new Location();
        var country = new Country();
        var region = new Region();

        var getEmployee = employee.GetAll();
        var getDepartment = department.GetAll();
        var getLocation = location.GetAll();
        var getCountry = country.GetAll();
        var getRegion = region.GetAll();

        switch (input)
        {
            case "1":

                var resultEmployeeJoin = (from e in getEmployee
                                          join d in getDepartment on e.DepartmentId equals d.Id
                                          join l in getLocation on d.LocationId equals l.Id
                                          join c in getCountry on l.CountryId equals c.Id
                                          join r in getRegion on c.RegionId equals r.Id
                                          select new EmployeeVM
                                          {
                                              Id = e.Id,
                                              FullName = e.FullName,
                                              Email = e.Email,
                                              PhoneNumber = e.PhoneNumber,
                                              Salary = e.Salary,
                                              DepartmentName = d.Name,
                                              StreetAddress = l.StreetAddress,
                                              CountryName = c.Name,
                                              RegionName = r.Name,

                                          }).ToList();
                GeneralMenu.List(resultEmployeeJoin, "Employee");
                break;
            case "2":
                var resultDepartmenJoin = (from e in getEmployee
                                          join d in getDepartment on e.DepartmentId equals d.Id
                                          group e by d.Name into departmentGroup
                                          where departmentGroup.Count() > 3
                                          select new DepartmentVM
                                          {
                                              DepartmentName = departmentGroup.Key,
                                              TotalEmployee = departmentGroup.Count(),
                                              MinSalary = departmentGroup.Min(e => e.Salary),
                                              MaxSalary = departmentGroup.Max(e => e.Salary),
                                              AverageSalary = departmentGroup.Average(e => e.Salary)
                                          }).ToList();
                GeneralMenu.List(resultDepartmenJoin, "Department");
                break;
            case "3":
                return false;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
        return true;
    }
}