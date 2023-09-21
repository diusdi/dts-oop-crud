namespace BasicConnectivity;

public class DepartmentVM
{

    public string DepartmentName { get; set; }
    public int TotalEmployee { get; set; }
    public int MinSalary { get; set; }
    public int MaxSalary { get; set; }
    public double AverageSalary { get; set; }

    public override string ToString()
    {
        return $"{DepartmentName} - {TotalEmployee} - {MinSalary} - {MaxSalary} - {AverageSalary}";
    }
}