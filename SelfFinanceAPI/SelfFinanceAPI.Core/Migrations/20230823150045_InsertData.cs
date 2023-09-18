using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Dapper;

#nullable disable

namespace SelfFinanceAPI.Core.Migrations
{
    /// <inheritdoc />
    public partial class InsertData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "appsettings.json");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configPath, optional: true, reloadOnChange: true)
                .Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute(
@"
        INSERT INTO ExpeseTypes (Name, IsIncome)
        VALUES 
            ('Salary', 1),
            ('Meal', 0),
            ('Additional income', 1),
            ('Transport', 0),
            ('Rent', 0),
            ('Utilities', 0),
            ('Groceries', 0),
            ('Entertainment', 0),
            ('Investment', 1),
            ('Healthcare', 0);

        INSERT INTO FinancialOperations (TypeId, Amount, DateTime, Description)
        VALUES 
            (1, 3000, '2023-09-07 10:00:00', 'Salary for September 7th'),
            (2, 20, '2023-09-07 12:30:00', 'Lunch'),
            (3, 500, '2023-09-07 15:45:00', 'Additional income'),
            (4, 10, '2023-09-07 18:20:00', 'Bus fare'),
            (5, 1200, '2023-09-07 20:00:00', 'Rent payment'),
            (6, 150, '2023-09-07 22:15:00', 'Utilities bill'),
            (7, 50, '2023-09-07 23:30:00', 'Grocery shopping'),
            (2, 20, '2023-09-07 18:30:00', 'Dinner'),
            (8, 40, '2023-09-07 19:00:00', 'Movie night'),
            (9, 1000, '2023-09-07 14:00:00', 'Investment deposit'),
            (10, 80, '2023-09-07 16:45:00', 'Doctor appointment'),
            (1, 3000, '2023-09-08 10:00:00', 'Salary for September 8th'),
            (2, 20, '2023-09-08 12:30:00', 'Lunch'),
            (3, 500, '2023-09-08 15:45:00', 'Additional income'),
            (4, 10, '2023-09-08 18:20:00', 'Bus fare'),
            (5, 1200, '2023-09-08 20:00:00', 'Rent payment'),
            (6, 150, '2023-09-08 22:15:00', 'Utilities bill'),
            (7, 50, '2023-09-08 23:30:00', 'Grocery shopping'),
            (8, 40, '2023-09-08 19:00:00', 'Movie night'),
            (9, 1000, '2023-09-08 14:00:00', 'Investment deposit'),
            (10, 80, '2023-09-08 16:45:00', 'Doctor appointment'),
            (1, 3000, '2023-09-09 10:00:00', 'Salary for September 9th'),
            (2, 20, '2023-09-09 12:30:00', 'Lunch'),
            (3, 500, '2023-09-09 15:45:00', 'Additional income'),
            (4, 10, '2023-09-09 18:20:00', 'Bus fare'),
            (5, 1200, '2023-09-09 20:00:00', 'Rent payment'),
            (6, 150, '2023-09-09 22:15:00', 'Utilities bill'),
            (7, 50, '2023-09-09 23:30:00', 'Grocery shopping'),
            (8, 40, '2023-09-09 19:00:00', 'Movie night'),
            (9, 1000, '2023-09-09 14:00:00', 'Investment deposit'),
            (10, 80, '2023-09-09 16:45:00', 'Doctor appointment'),
            (1, 3000, '2023-09-10 10:00:00', 'Salary for September 10th'),
            (2, 20, '2023-09-10 12:30:00', 'Lunch'),
            (3, 500, '2023-09-10 15:45:00', 'Additional income'),
            (4, 10, '2023-09-10 18:20:00', 'Bus fare'),
            (5, 1200, '2023-09-10 20:00:00', 'Rent payment'),
            (6, 150, '2023-09-10 22:15:00', 'Utilities bill'),
            (7, 50, '2023-09-10 23:30:00', 'Grocery shopping'),
            (8, 40, '2023-09-10 19:00:00', 'Movie night'),
            (9, 1000, '2023-09-10 14:00:00', 'Investment deposit'),
            (10, 80, '2023-09-10 16:45:00', 'Doctor appointment'),
            (1, 3000, '2023-09-11 10:00:00', 'Salary for September 11th'),
            (2, 20, '2023-09-11 12:30:00', 'Lunch'),
            (3, 500, '2023-09-11 15:45:00', 'Additional income'),
            (4, 10, '2023-09-11 18:20:00', 'Bus fare'),
            (5, 1200, '2023-09-11 20:00:00', 'Rent payment'),
            (6, 150, '2023-09-11 22:15:00', 'Utilities bill'),
            (7, 50, '2023-09-11 23:30:00', 'Grocery shopping'),
            (8, 40, '2023-09-11 19:00:00', 'Movie night'),
            (9, 1000, '2023-09-11 14:00:00', 'Investment deposit'),
            (10, 80, '2023-09-11 16:45:00', 'Doctor appointment'),
            (1, 3000, '2023-09-12 10:00:00', 'Salary for September 12th'),
            (2, 20, '2023-09-12 12:30:00', 'Lunch'),
            (3, 500, '2023-09-12 15:45:00', 'Additional income'),
            (4, 10, '2023-09-12 18:20:00', 'Bus fare'),
            (5, 1200, '2023-09-12 20:00:00', 'Rent payment'),
            (6, 150, '2023-09-12 22:15:00', 'Utilities bill'),
            (7, 50, '2023-09-12 23:30:00', 'Grocery shopping'),
            (8, 40, '2023-09-12 19:00:00', 'Movie night'),
            (9, 1000, '2023-09-12 14:00:00', 'Investment deposit'),
            (10, 80, '2023-09-12 16:45:00', 'Doctor appointment'),
            (1, 3000, '2023-09-13 10:00:00', 'Salary for September 13th'),
            (2, 20, '2023-09-13 12:30:00', 'Lunch'),
            (3, 500, '2023-09-13 15:45:00', 'Additional income'),
            (4, 10, '2023-09-13 18:20:00', 'Bus fare'),
            (5, 1200, '2023-09-13 20:00:00', 'Rent payment'),
            (6, 150, '2023-09-13 22:15:00', 'Utilities bill'),
            (7, 50, '2023-09-13 23:30:00', 'Grocery shopping'),
            (8, 40, '2023-09-13 19:00:00', 'Movie night'),
            (9, 1000, '2023-09-13 14:00:00', 'Investment deposit'),
            (10, 80, '2023-09-13 16:45:00', 'Doctor appointment');
    ");
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
