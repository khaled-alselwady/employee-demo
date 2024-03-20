using System.Configuration;

namespace EmployeeDemo_Business_DataAccess
{
    static class clsDataAccessSettings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    }
}