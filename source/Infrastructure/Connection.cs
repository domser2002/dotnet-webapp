using Microsoft.Data.SqlClient;

namespace Infrastructure
{
    public static class Connection
    {
        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = "chmurzynskim.database.windows.net",
                UserID = "database_admin",
                Password = "dawid_to_koks1234",
                InitialCatalog = "NET"
            };
            return builder.ConnectionString;
        }
    }
}
