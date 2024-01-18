using Microsoft.Data.SqlClient;

namespace Infrastructure
{
    public static class FakeConnection
    {
        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = "dot-net-webapp.database.windows.net",
                UserID = "database_admin",
                Password = "dawid_to_koks1234",
                InitialCatalog = "CourierHubTest"
            };
            return builder.ConnectionString;
        }
    }
}
