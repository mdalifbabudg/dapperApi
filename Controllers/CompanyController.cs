using Microsoft.AspNetCore.Mvc;

[Route("api/company")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly static string UPDATE_SUCCESSFULLY = "Successfully Updated!";
    private readonly static string DELETE_SUCCESSFULLY = "Successfully Deleted!";

    private readonly CompanySerivce companyService;

    public CompaniesController(CompanySerivce companyService)
    {
        this.companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        try
        {
            var companies = await companyService.GetCompanies();
            return Ok(companies);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}", Name = "CompanyById")]
    public async Task<IActionResult> GetCompany(int id){
        try
        {
            var company = await companyService.GetCompany(id);
            if(company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }
        catch(Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCompany(CompanyCreationDto company)
    {
        try
        {
            var createdCompany = await companyService.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }
        catch(Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, CompanyUpdateDto company)
    {
        try
        {
            var dbCompany = await companyService.GetCompany(id);
            if(dbCompany == null)
            {
                return NotFound();
            }

            await companyService.UpdateCompany(id, company);
            return Ok(UPDATE_SUCCESSFULLY);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        try
        {
            var dbCompany = await companyService.GetCompany(id);
            if(dbCompany == null)
            {
                return NotFound();
            }

            await companyService.DeleteCompany(id);
            return Ok(DELETE_SUCCESSFULLY);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}