﻿using System;
using System.Data.SqlClient;
using FluentMigrator.Runner;
using Infrastructure.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace Test.Infrastructure.Common.Database
{
    public class DropCreateDatabase : IDisposable
    {
        private const string DatabaseName = "Fabio_Test";

        private const string ConnectionString = @"Server=tcp:localhost,1433;Persist Security Info=False;User ID=sa;Password=Fabio1234;MultipleActiveResultSets=False;Connection Timeout=30;";
        private static readonly string _connectionStringWithDatabase = $"Server=tcp:localhost,1433;Initial Catalog={DatabaseName};Persist Security Info=False;User ID=sa;Password=Fabio1234;MultipleActiveResultSets=False;Connection Timeout=30;";

        public DropCreateDatabase()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                new SqlCommand($"DROP DATABASE IF EXISTS {DatabaseName}", connection).ExecuteNonQuery();
                new SqlCommand($"CREATE DATABASE {DatabaseName}", connection).ExecuteNonQuery();
            }

            RunMigrations();
        }

        private void RunMigrations()
        {
            var serviceProvider = CreateServices();

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
              // Add common FluentMigrator services
              .AddFluentMigratorCore()
              .ConfigureRunner(rb => rb
                  // Add SQL Server 2016 support to FluentMigrator
                  .AddSqlServer2016()
                  // Set the connection string
                  .WithGlobalConnectionString(_connectionStringWithDatabase)
                  // Define the assembly containing the migrations
                  .ScanIn(typeof(M202011171800Task).Assembly).For.Migrations())
              // Enable logging to console in the FluentMigrator way
              .AddLogging(lb => lb.AddFluentMigratorConsole())
              // Build the service provider
              .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
        }

        public void Dispose()
        {
        }
    }
}
