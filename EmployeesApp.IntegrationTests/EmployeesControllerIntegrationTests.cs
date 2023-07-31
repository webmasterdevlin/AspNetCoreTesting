using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EmployeesApp.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EmployeesApp.IntegrationTests;

public class EmployeesControllerIntegrationTests : IClassFixture<TestingWebAppFactory>
{
    private readonly HttpClient _client;
    private readonly IServiceScope _scope;
    private readonly EmployeeContext _context;

    public EmployeesControllerIntegrationTests(TestingWebAppFactory factory)
    {
        _client = factory.CreateClient();

        // Create a scope to retrieve scoped services
        _scope = factory.Services.CreateScope();
        _context = _scope.ServiceProvider.GetRequiredService<EmployeeContext>();

        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        // Delete existing data
        _context.Database.EnsureDeleted();

        // Ensure the database is created
        _context.Database.EnsureCreated();
        
        _context.SaveChanges();
    }

    // public void Dispose()
    // {
    //     // Clean up the database after each test
    //     _context.Database.EnsureDeleted();
    //
    //     // Dispose of the scope
    //     _scope.Dispose();
    // }

    
    [Fact]
    public async Task Index_WhenCalled_ReturnsApplicationForm()
    {
        var response = await _client.GetAsync("/Employees");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        // xUnit assertions
        Assert.Contains("Mark", responseString);
        Assert.Contains("Evelin", responseString);
        
        // FluentAssertions assertions
        responseString.Should().Contain("Mark");
        responseString.Should().Contain("Evelin");
    }

    [Fact]
    public async Task Create_WhenCalled_ReturnsCreateForm()
    {
        var response = await _client.GetAsync("/Employees/Create");

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        // xUnit assertions
        Assert.Contains("Please provide a new employee data", responseString);
        
        // FluentAssertions assertions
        responseString.Should().Contain("Please provide a new employee data");
    }

    [Fact]
    public async Task Create_SentWrongModel_ReturnsViewWithErrorMessages()
    {
        var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Employees/Create");

        var formModel = new Dictionary<string, string>
            {
                { "Name", "New Employee" },
                { "Age", "25" }
            };

        postRequest.Content = new FormUrlEncodedContent(formModel);

        var response = await _client.SendAsync(postRequest);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        // xUnit assertions
        Assert.Contains("Account number is required", responseString);
        
        // FluentAssertions assertions
        responseString.Should().Contain("Account number is required");
    }

    [Fact]
    public async Task Create_WhenPOSTExecuted_ReturnsToIndexViewWithCreatedEmployee()
    {
        var postRequest = new HttpRequestMessage(HttpMethod.Post, "/Employees/Create");

        var formModel = new Dictionary<string, string>
            {
                { "Name", "New Employee" },
                { "Age", "25" },
                { "AccountNumber", "214-5874986532-21" }
            };

        postRequest.Content = new FormUrlEncodedContent(formModel);

        var response = await _client.SendAsync(postRequest);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        // xUnit assertions
        Assert.Contains("New Employee", responseString);
        Assert.Contains("214-5874986532-21", responseString);
        
        // FluentAssertions assertions
        responseString.Should().Contain("New Employee");
        responseString.Should().Contain("214-5874986532-21");
    }
    
    [Fact]
    public async Task Delete_WhenDELETExecuted_DeletesEmployeeAndReturnsToIndexView()
    {
        var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, "/Employees/Delete/3c904a3a-91eb-4d3c-b332-b1315d929875");

        var response = await _client.SendAsync(deleteRequest);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        
        // xUnit assertions
        Assert.DoesNotContain("Updated Employee", responseString);
        Assert.DoesNotContain("987-6543210000-98", responseString);
        
        // FluentAssertions assertions
        responseString.Should().NotContain("Updated Employee");
        responseString.Should().NotContain("987-6543210000-98");
    }

    
    [Fact]
    public async Task Update_WhenPUTExecuted_UpdatesEmployeeAndReturnsToIndexView()
    {
        var putRequest = new HttpRequestMessage(HttpMethod.Put, "/Employees/Update");

        var formModel = new Dictionary<string, string>
        {
            { "Id", "3c904a3a-91eb-4d3c-b332-b1315d929875" },
            { "Name", "Updated Employee" },
            { "Age", "35" },
            { "AccountNumber", "987-6543210000-98" }
        };

        putRequest.Content = new FormUrlEncodedContent(formModel);

        var response = await _client.SendAsync(putRequest);

        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();

        // xUnit assertions
        Assert.Contains("Updated Employee", responseString);
        Assert.Contains("987-6543210000-98", responseString);
    
        // FluentAssertions assertions
        responseString.Should().Contain("Updated Employee");
        responseString.Should().Contain("987-6543210000-98");
    }
}