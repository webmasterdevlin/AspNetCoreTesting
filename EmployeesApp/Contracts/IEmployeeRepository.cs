using EmployeesApp.Models;

namespace EmployeesApp.Contracts;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetAll();

    Employee GetEmployee(Guid id);

    void CreateEmployee(Employee employee);

    // TODO: DeleteEmployee
    void DeleteEmployee(Guid id);
    
    // TODO: UpdateEmployee
    void UpdateEmployee(Employee employee);
}