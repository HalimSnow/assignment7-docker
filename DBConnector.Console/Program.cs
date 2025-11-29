using DBConnector;
using System.Data;

namespace DBConnector.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("=== Database Connection Tester ===\n");

            while (true)
            {
                // Get database type
                System.Console.WriteLine("Select database type:");
                System.Console.WriteLine("1. MongoDB");
                System.Console.WriteLine("2. PostgreSQL");
                System.Console.WriteLine("3. Exit");
                System.Console.Write("\nEnter your choice (1-3): ");

                string? choice = System.Console.ReadLine();

                if (choice == "3")
                {
                    System.Console.WriteLine("\nExiting application. Goodbye!");
                    break;
                }

                // Get connection string
                System.Console.Write("\nEnter connection string: ");
                string? connectionString = System.Console.ReadLine();

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    System.Console.WriteLine("Error: Connection string cannot be empty.\n");
                    continue;
                }

                // Create appropriate connector and test connection
                IDBConnector? connector = null;

                try
                {
                    if (choice == "1")
                    {
                        System.Console.WriteLine("\nTesting MongoDB connection...");
                        connector = new MongoConnector(connectionString);
                    }
                    else if (choice == "2")
                    {
                        System.Console.WriteLine("\nTesting PostgreSQL connection...");
                        connector = new PostgresConnector(connectionString);
                    }
                    else
                    {
                        System.Console.WriteLine("Error: Invalid choice. Please select 1, 2, or 3.\n");
                        continue;
                    }

                    // Attempt to ping the database
                    bool pingResult = await connector.ping();

                    if (pingResult)
                    {
                        System.Console.WriteLine("✓ Connection successful! Database is reachable.\n");
                    }
                    else
                    {
                        System.Console.WriteLine("✗ Connection failed! Unable to reach database.\n");
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"✗ Error: {ex.Message}\n");
                }
            }
        }
    }
}