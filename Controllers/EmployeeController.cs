using Microsoft.AspNetCore.Mvc;

[Route("api/employee")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService employeeService;
    public EmployeeController(EmployeeService employeeService)
    {
        this.employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        try
        {
            var employees = await employeeService.GetEmployees();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            //log error
            return StatusCode(500, ex.Message);
        }
    }
}
