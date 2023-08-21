using Dapper;
public class CompanyRepository : ICompanyRepository
{
    private readonly DapperContext context;

    public CompanyRepository(DapperContext context)
    {
        this.context = context;
    }

    public async Task<Company> CreateCompany(CompanyCreationDto company)
    {
        var query = "INSERT INTO Companies ( Name, Address, Country) VALUES (@Name, @Address, @Country)" +
        "SELECT CAST(SCOPE_IDENTITY() as int)";

        var parameters = new DynamicParameters();
        parameters.Add("Name", company.Name, System.Data.DbType.String);
        parameters.Add("Address", company.Address, System.Data.DbType.String);
        parameters.Add ("Country", company.Country, System.Data.DbType.String);

        using(var connection = context.CreateConnection())
        {
            var id = await connection.QuerySingleAsync<int>(query, parameters);

            var createdCompany = new Company
            {
                Id = id,
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };

            return createdCompany;
        }
    }

    public async Task DeleteCompany(int id)
    {
        var query = "DELETE FROM Companies WHERE Id = @Id";

        using (var connection = context.CreateConnection())
        {
            await connection.ExecuteAsync(query, new { id });
        }
    }

    public async Task<IEnumerable<Company>> GetCompanies()
    {
        var query = "SELECT * FROM Companies";
        using (var connection = context.CreateConnection())
        {
            var companies = await connection.QueryAsync<Company>(query);
            return companies.ToList();
        }
    }

    public async Task<Company> GetCompany(int id)
    {
        var query = "SELECT * FROM Companies WHERE Id = @Id; " +
        "SELECT * FROM Employees WHERE CompanyId = @Id";

        using (var connection = context.CreateConnection())
        using (var multi = await connection.QueryMultipleAsync(query, new { id }))
        {
            var company = await multi.ReadSingleOrDefaultAsync<Company>();
            if(company != null)
            {
                company.Employees = (await multi.ReadAsync<Employee>()).ToList();
            }
            return company;
        }
    }

    public async Task UpdateCompany(int id, CompanyUpdateDto company)
    { 
        var query = "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("Id", id, System.Data.DbType.Int32);
        parameters.Add("Name", company.Name, System.Data.DbType.String);
        parameters.Add("Address", company.Address, System.Data.DbType.String);
        parameters.Add ("Country", company.Country, System.Data.DbType.String);

        using (var connection = context.CreateConnection())
        {
            await connection.ExecuteAsync(query, parameters);
        }
    }
}