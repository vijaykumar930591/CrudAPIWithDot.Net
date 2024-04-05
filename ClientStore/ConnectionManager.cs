using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public static class ConnectionManager
{
    public static SqlConnection CreateSqlConnection()
    {
        // Load the connection string from configuration
        string connectionString = GetConnectionString();

        // Create a new SqlConnection with the connection string
        return new SqlConnection(connectionString);
    }

    private static string GetConnectionString()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        // Adjust the configuration key based on your appsettings.json structure
        return configuration.GetConnectionString("DefaultConnection");
    }
}
