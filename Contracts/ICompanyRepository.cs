public interface ICompanyRepository
{
    public Task<IEnumerable<Company>> GetCompanies();
    public Task<Company> GetCompany(int id);
    public Task<Company> CreateCompany(CompanyCreationDto company);
    public Task UpdateCompany(int id, CompanyUpdateDto company);
    public Task DeleteCompany(int id);
}