public interface IEmployeeRepository
{
    public Task<IEnumerable<Employee>> GetEmployees();
}