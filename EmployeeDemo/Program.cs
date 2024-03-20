using EmployeeDemo_Business;
using System;
using System.Data;

class Program
{
    struct strEmployeeInfo
    {
        public int HighRatingEmployeeCount;
        public int MediumRatingEmployeeCount;
        public int LowRatingEmployeeCount;

        public double TotalSalariesOfHighRatingEmployees;
        public double TotalSalariesOfMediumRatingEmployees;
        public double TotalSalariesOfLowRatingEmployees;
    }
    static strEmployeeInfo EmployeeInfo = new strEmployeeInfo();

    static void _PrintAllEmployees()
    {
        DataTable dtEmployees = clsEmployee.GetAllEmployees();

        foreach (DataRow drEmployee in dtEmployees.Rows)
        {
            Console.WriteLine($"Name: {drEmployee["Name"]}\nDepartment: {drEmployee["Department"]}\nSalary: ${Convert.ToInt32(drEmployee["Salary"]).ToString("N")}");
            Console.WriteLine("------------------------------\n");
        }
    }

    static void _UpdateEmployeesSalaryWithoutCaseStatement()
    {
        DataTable dtEmployees = clsEmployee.GetAllEmployees();
        double CurrentSalary = 0;
        double NewSalary = 0;

        foreach (DataRow drEmployee in dtEmployees.Rows)
        {
            CurrentSalary = (int)(drEmployee["Salary"]);

            switch ((int)drEmployee["PerformanceRating"])
            {
                case int Rating when Rating > 90:
                    NewSalary = CurrentSalary * clsEmployee.clsIncreasingRate.Over90;
                    break;


                case int Rating when Rating >= 75 && Rating < 90:
                    NewSalary = CurrentSalary * clsEmployee.clsIncreasingRate.Between75And90;
                    break;


                case int Rating when Rating >= 50 && Rating < 75:
                    NewSalary = CurrentSalary * clsEmployee.clsIncreasingRate.Between50And74;
                    break;
            }

            clsEmployee.UpdateSalaryWithoutUsingCaseStatement(drEmployee["Name"].ToString(), NewSalary);
        }
    }

    static void _UpdateEmployeesSalaryUsingCaseStatement()
    {
        clsEmployee.UpdateSalaryUsingCaseStatement();
    }

    static void _SortingEmployeesWithoutCaseStatement()
    {
        DataTable dtEmployees = clsEmployee.GetAllEmployees();

        foreach (DataRow drEmployee in dtEmployees.Rows)
        {
            switch ((int)drEmployee["PerformanceRating"])
            {

                case int Rating when Rating >= 80:
                    EmployeeInfo.HighRatingEmployeeCount++;
                    EmployeeInfo.TotalSalariesOfHighRatingEmployees += (int)(drEmployee["Salary"]);
                    break;

                case int Rating when Rating >= 60:
                    EmployeeInfo.MediumRatingEmployeeCount++;
                    EmployeeInfo.TotalSalariesOfMediumRatingEmployees += (int)(drEmployee["Salary"]);
                    break;

                default:
                    EmployeeInfo.LowRatingEmployeeCount++;
                    EmployeeInfo.TotalSalariesOfLowRatingEmployees += (int)(drEmployee["Salary"]);
                    break;

            }
        }

    }

    static void _SortingEmployeesUsingCaseStatement()
    {
        DataTable dtEmployees = clsEmployee.SortingEmployeesBasedOnPerformanceCategory();

        foreach (DataRow drEmployee in dtEmployees.Rows)
        {
            Console.WriteLine($"{drEmployee["PerformanceCategory"]} \t\t\t\t{drEmployee["NumberOfEmployees"]} \t\t\t\t{Convert.ToInt32(drEmployee["AverageSalary"]):C2}");
        }
    }

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Performance Category \t\tNumberOfEmployees \t\tAverageSalary");
        Console.ResetColor();

        _SortingEmployeesWithoutCaseStatement();
        Console.WriteLine($"High \t\t\t\t{EmployeeInfo.HighRatingEmployeeCount} \t\t\t\t{(EmployeeInfo.TotalSalariesOfHighRatingEmployees / EmployeeInfo.HighRatingEmployeeCount):C2}");
        Console.WriteLine($"Medium \t\t\t\t{EmployeeInfo.MediumRatingEmployeeCount} \t\t\t\t{(EmployeeInfo.TotalSalariesOfMediumRatingEmployees / EmployeeInfo.MediumRatingEmployeeCount):C2}");
        Console.WriteLine($"Low \t\t\t\t{EmployeeInfo.LowRatingEmployeeCount} \t\t\t\t{(EmployeeInfo.TotalSalariesOfLowRatingEmployees / EmployeeInfo.LowRatingEmployeeCount):C2}");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n\nPerformance Category \t\tNumberOfEmployees \t\tAverageSalary");
        Console.ResetColor();

        _SortingEmployeesUsingCaseStatement();

        Console.ReadKey();
    }
}


