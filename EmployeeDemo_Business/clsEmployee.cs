using EmployeeDemo_Business_DataAccess;
using System.Data;

namespace EmployeeDemo_Business
{
    public class clsEmployee
    {
        public static class clsIncreasingRate
        {
            public const double Over90 = 1.15;
            public const double Between75And90 = 1.10;
            public const double Between50And74 = 1.05;
        }

        public static bool UpdateSalaryWithoutUsingCaseStatement(string Name, double NewSalary)
        {
            return clsEmployeeData.UpdateSalaryWithoutCaseStatement(Name, NewSalary);
        }

        public static bool UpdateSalaryUsingCaseStatement()
        {
            return clsEmployeeData.UpdateSalaryUsingCaseStatement();
        }

        public static DataTable SortingEmployeesBasedOnPerformanceCategory()
        {
            return clsEmployeeData.SortingEmployeesBasedOnPerformanceCategory();
        }

        public static DataTable GetAllEmployees()
        {
            return clsEmployeeData.GetAllEmployees();
        }
    }

}
