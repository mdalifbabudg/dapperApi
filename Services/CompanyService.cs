public class CompanySerivce
{
    private readonly ICompanyRepository companyRepository;
    private readonly EmployeeService employeeService;

    public CompanySerivce(ICompanyRepository companyRepository, EmployeeService employeeService)
    {
        this.companyRepository = companyRepository;
        this.employeeService = employeeService;
    }
    public async Task<IEnumerable<Company>> GetCompanies()
    {
        return await companyRepository.GetCompanies();
    }

    public async Task<Company> GetCompany(int companyId)
    {
        return await companyRepository.GetCompany(companyId);
    }

    public async Task<Company> CreateCompany(CompanyCreationDto company)
    {
        return await companyRepository.CreateCompany(company);
    }

    public async Task UpdateCompany(int id, CompanyUpdateDto company)
    {
        await companyRepository.UpdateCompany(id, company);
    }

    public async Task DeleteCompany(int id){
        await companyRepository.DeleteCompany(id);
    }
}