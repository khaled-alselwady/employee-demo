using System;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDemo_Business_DataAccess
{
    public class clsEmployeeData
    {
        public static bool UpdateSalaryWithoutCaseStatement(string Name, double NewSalary)
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    string query = @"Update Employees2
                                     set Salary = @NewSalary
                                     where Name = @Name";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", (object)Name ?? DBNull.Value);
                        command.Parameters.AddWithValue("@NewSalary", NewSalary);

                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogError.LogError("Database Exception", ex);
            }
            catch (Exception ex)
            {
                clsLogError.LogError("General Exception", ex);
            }

            return (RowAffected > 0);
        }

        public static bool UpdateSalaryUsingCaseStatement()
        {
            int RowAffected = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    string query = @"update Employees2
                                     set Salary = case when PerformanceRating > 90 then Salary * 1.15
                                                       when PerformanceRating between 75 and 90 then Salary * 1.10
                                                       when PerformanceRating between 50 and 74 then Salary * 1.05
                                                       else Salary
                                                  end";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        RowAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogError.LogError("Database Exception", ex);
            }
            catch (Exception ex)
            {
                clsLogError.LogError("General Exception", ex);
            }

            return (RowAffected > 0);
        }

        public static DataTable SortingEmployeesBasedOnPerformanceCategory()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    string query = @"select PerformanceCategory, count(*) as NumberOfEmployees, avg(Salary) as AverageSalary
from
(select Salary,
       case
		   when PerformanceRating >= 80 then 'High'
		   when PerformanceRating >= 60 then 'Medium' 
		   else 'Low'
	   end as PerformanceCategory
from Employees2) PerformanceTable
group by PerformanceCategory";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogError.LogError("Database Exception", ex);
            }
            catch (Exception ex)
            {
                clsLogError.LogError("General Exception", ex);
            }

            return dt;
        }

        public static DataTable GetAllEmployees()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    connection.Open();

                    string query = @"select * from Employees2";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                clsLogError.LogError("Database Exception", ex);
            }
            catch (Exception ex)
            {
                clsLogError.LogError("General Exception", ex);
            }

            return dt;
        }
    }
}
