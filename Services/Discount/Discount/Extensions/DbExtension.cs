using Npgsql;

namespace Discount.Extensions
{
    public static class DbExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Discount DB Migration started...");
                    ApplyMigration(config);
                    logger.LogInformation("Discount DB Migration completed.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database.");
                    throw;
                }
            }
            return host;
        }

        private static void ApplyMigration(IConfiguration config)
        {
            var retry = 5;
            while(retry > 0)
            {
                try
                {
                    using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();
                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };
                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();
                    command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                        ProductName VARCHAR(50) NOT NULL,
                                        Description TEXT,
                                        Amount INT)";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Adidas FIFA World Cup 2018 OMB Football', 'Shoe Discount', 1000);";
                    command.ExecuteNonQuery();
                    command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Yonex VCORE Pro 100 A Tenis Racquet', 'Racquet Discount', 2000);";
                    command.ExecuteNonQuery();
                    connection.Close();
                    break;
                }
                catch (NpgsqlException ex)
                {
                    retry--;
                    if (retry == 0)
                    {
                        throw;
                    }
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
