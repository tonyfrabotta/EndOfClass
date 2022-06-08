namespace DemoApi.Models;

public class StatusResponse
{
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt
    {
        get; set;
    }

}

public class DeveloperInfo
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
