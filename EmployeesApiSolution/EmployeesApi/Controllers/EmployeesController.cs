namespace EmployeesApi.Controllers;

[ApiController]
[Produces("application/json")]
public class EmployeesController : ControllerBase
{
    private readonly ILookupEmployees _employeeLookup;
    private readonly IManageEmployees _employeeCommands;

    public EmployeesController(ILookupEmployees employeeLookup, IManageEmployees employeeCommands)
    {
        _employeeLookup = employeeLookup;
        _employeeCommands = employeeCommands;
    }

    [HttpPut("/employees/{id:bsonid}/name")]
    public async Task<ActionResult> ChangeNameAsync(string id, [FromBody] EmployeeNameInformation name)
    {
        bool didChange = await _employeeCommands.UpdateNameAsync(id, name);
        return didChange ? NoContent() : NotFound();
    }



    [HttpPut("/employees/{id:bsonid}/department")]
    public async Task<ActionResult> ChangeDepartmentAsync(string id, [FromBody] string department)
    {
        bool didChange = await _employeeCommands.UpdateDepartmentAsync(id, department);
        return didChange ? NoContent() : NotFound();
    } 

    [HttpDelete("/employees/{id:bsonid}")]
    [ProducesResponseType(204)]
    public async Task<ActionResult> RemoveEmployee(string id)
    {
        await _employeeCommands.FireAsync(id);
        return NoContent(); // "Fine"
    }

    [HttpPost("/employees")]
    [ProducesResponseType(201)]
    public async Task<ActionResult<EmployeeDocumentResponse>> HireEmployeeAsync([FromBody] EmployeeCreateRequest request)
    {

       
        EmployeeDocumentResponse response = await _employeeCommands.CreateEmployeeAsync(request);
       
        return CreatedAtRoute("employee#getemployeebyid", new
        {
            id = response.Id
        }, response);
    }

    [HttpGet("/employees")]
    public async Task<ActionResult<CollectionResponse<EmployeeSummaryResponse>>> GetEmployeesCollectionAsync()
    {

        List<EmployeeSummaryResponse> data = await  _employeeLookup.GetAllEmployeeSummariesAsync();

        var response = new CollectionResponse<EmployeeSummaryResponse> { Data = data };
        return Ok(response);
    }


    /// <summary>
    /// Use this to look up an employee.
    /// </summary>
    /// <param name="id">The id of the Employee, must be a valid BSON identifier.</param>
    /// <returns></returns>
    [HttpGet("/employees/{id:bsonid}", Name ="employee#getemployeebyid")]
    [ProducesResponseType(404)]
    [ProducesResponseType(200)]
    public async Task<ActionResult<EmployeeDocumentResponse>> GetEmployeeByIdAsync(string id)
    {
       
        EmployeeDocumentResponse response = await _employeeLookup.GetEmployeeByIdAsync(id);
        if (response == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(response);
        }
    }
}
