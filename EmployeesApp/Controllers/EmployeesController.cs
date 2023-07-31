using EmployeesApp.Contracts;
using EmployeesApp.Models;
using EmployeesApp.Validation;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeesApp.Controllers;

public class EmployeesController : Controller
{
    private readonly IEmployeeRepository _repo;
    private readonly AccountNumberValidation _validation;

    public EmployeesController(IEmployeeRepository repo)
    {
        _repo = repo;
        _validation = new AccountNumberValidation();
    }

    public IActionResult Index()
    {
        var employees = _repo.GetAll();
        return View(employees);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    
    public IActionResult Details(Guid id)
    {
        var employee = _repo.GetEmployee(id);
        return View(employee);
    }

    [HttpPost]
    public IActionResult Create([Bind("Name,AccountNumber,Age")] Employee employee)
    {
        if (!ModelState.IsValid)
        {
            return View(employee);
        }

        if (employee.AccountNumber != null && !_validation.IsValid(employee.AccountNumber))
        {
            ModelState.AddModelError("AccountNumber", "Account Number is invalid");
            return View(employee);
        }

        _repo.CreateEmployee(employee);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public IActionResult Delete(Guid id)
    {
        _repo.DeleteEmployee(id);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public IActionResult Update(Employee employee)
    {
        if (employee.AccountNumber != null && !_validation.IsValid(employee.AccountNumber))
        {
            ModelState.AddModelError("AccountNumber", "Account Number is invalid");
        }
        
        _repo.UpdateEmployee(employee);
        return RedirectToAction(nameof(Index));
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}