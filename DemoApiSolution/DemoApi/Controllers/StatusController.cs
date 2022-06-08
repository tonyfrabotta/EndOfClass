
namespace DemoApi.Controllers;

public class StatusController : ControllerBase
{

    private readonly ILookupCurrentStatus _statusLookup;
    private readonly ILookupDevelopers _developerLookup;

    public StatusController(ILookupCurrentStatus statusLookup, ILookupDevelopers developerLookup)
    {
        _statusLookup = statusLookup;
        _developerLookup = developerLookup;
    }


    // GET /status
    [HttpGet("/status")]
    public async Task<ActionResult<StatusResponse>> GetStatus()
    {

        // "Write the Code you Wish You Had" (WTCYWYH)
        StatusResponse response = await _statusLookup.GetCurrentStatusAsync();
        return Ok(response); // reponse with 200 Ok status code.
    }

    [HttpGet("/status/oncalldeveloper")]
    public async Task<ActionResult<DeveloperInfo>> GetOnCallDeveloper()
    {
        
        DeveloperInfo response = await _developerLookup.GetOnCallDeveloperAsync();
        return Ok(response);
    }
}


