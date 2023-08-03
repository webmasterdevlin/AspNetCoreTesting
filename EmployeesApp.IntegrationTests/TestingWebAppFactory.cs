using EmployeesApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace EmployeesApp.IntegrationTests;

/// <summary>
/// The TestingWebAppFactory class is typically used in conjunction with the WebApplicationFactory in integration tests to configure the application in a controlled manner.
/// This specific implementation provides a pattern for replacing a real database connection with an in-memory database, making it useful for testing data access code without affecting the real database.
/// Inherits from WebApplicationFactory, a class provided by ASP.NET Core for bootstrapping a test server that mimics the behavior of the actual web application.
/// </summary>
public class TestingWebAppFactory : WebApplicationFactory<Program>
{
    /// <summary>
    /// This method is used to customize the web host for testing purposes. It's overriding a method from the base class that's called when the test server is being set up.
    /// </summary>
    /// <param name="builder"></param>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Finding the Existing DbContext: It looks for an existing registration of DbContextOptions<EmployeeContext> in the dependency injection container.
            // This would typically be a registration for the real database connection.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<EmployeeContext>));
            // Removing Existing DbContext: If an existing registration is found, it's removed.
            // This is done to replace the real database connection with an in-memory version.
            if (descriptor != null)
                services.Remove(descriptor);
            // Adding In-Memory DbContext: It registers a new DbContextOptions<EmployeeContext> that's configured to use an in-memory database.
            // This ensures that the tests don't affect the actual database, making them isolated and repeatable.
            services.AddDbContext<EmployeeContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryMvcTestDb");
            });
            // Building the Service Provider: Builds the service provider to access the services.
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            // Creating the Database: Using the in-memory EmployeeContext, it ensures the database is created.
            // This is wrapped in a try-catch block, allowing for potential error handling (currently just rethrows the exception).
            using var appContext = scope.ServiceProvider.GetRequiredService<EmployeeContext>();
            try
            {
                appContext.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                //Log errors or do anything you think it's needed
                throw;
            }
        });
    }
}

// Potential Usage:
// This class would be used in integration tests, possibly in conjunction with tools like xUnit.
// It allows tests to run using the actual Startup configuration of the application, but with specific changes that make the tests more controlled and isolated.
// The in-memory database ensures that tests can run quickly without dependencies on external database servers, and without side effects that could affect other tests or the real application behavior.