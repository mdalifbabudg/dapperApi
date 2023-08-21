using Dapper;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DapperContext context;

    public EmployeeRepository(DapperContext context) 
    {
        this.context = context;
    } 
    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        var query = "SELECT * FROM Employees";
        using(var connection = context.CreateConnection())
        {
            var employees = await connection.QueryAsync<Employee>(query);
            return employees.ToList();
        }
    }
}