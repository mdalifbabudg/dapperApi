public class EmployeeService
{
    private readonly IEmployeeRepository employeeRepository;
    
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        return await employeeRepository.GetEmployees();
    }
}