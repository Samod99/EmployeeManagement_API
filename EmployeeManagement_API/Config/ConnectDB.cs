using System.Data.SqlClient;

namespace EmployeeManagement_API.Config
{
    public class ConnectDB
    {
        public static string getConnectionString()
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = @"MSI\SQLEXPRESS";
            csb.InitialCatalog = "EmployeeManagement_DB";
            csb.IntegratedSecurity = true;

            return csb.ToString();
        }
    }
}
