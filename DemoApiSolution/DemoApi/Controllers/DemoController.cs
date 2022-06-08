namespace DemoApi.Controllers;

public class DemoController : ControllerBase
{
    // GET /sayhi/bob "the path"
    // GET /sayhi/sue

    [HttpGet("/sayhi/{yourName}")]
    public ActionResult SayHello(string yourName)
    {
        return Ok($"Hello, {yourName}");
    }

    [HttpGet("/blogs/{year:int:min(1999)}")]
    public ActionResult GetBlogs(int year)
    {
        return Ok($"Here are the blog posts for {year}");
    }

    [HttpGet("/blogs/{year:int}/{month:int}/{day:int}")]
    public ActionResult DetailedBlogs(int year, int month, int day)
    {
        return Ok($"{year}/{month}/{day}");
    }

    // GET /employees
    // GET /employees?dept=DEV
    [HttpGet("/employees")]
    public ActionResult GetEmployees([FromQuery] string dept = "All")
    {
        return Ok($"Showing {dept} employees");
    }

    [HttpGet("/whoami")]
    public ActionResult WhoAmi([FromHeader(Name ="User-Agent")]string userAgent)
    {

        return Ok($"You are running {userAgent}");
    }

    [HttpGet("/status2")]
    public async Task<ActionResult> GetStatus2([FromServices] ILookupCurrentStatus status)
    {
        var response = await status.GetCurrentStatusAsync();
        return Ok(response);
    }

    [HttpGet("/sayhi2")] // This is super creepy, don't do this. (Use a GET with a body)
    public ActionResult SayHi2([FromBody] string name)
    {
        return Ok($"Hello, {name}");
    }

    [HttpPost("/employees")]
    public ActionResult Hire([FromBody] EmployeeRequest request)
    {
        return StatusCode(201, request);
    }

}

public class EmployeeRequest
{
    public string Name
    {
        get; set;
    }

    public decimal Salary
    {
        get; set;
    }
}
