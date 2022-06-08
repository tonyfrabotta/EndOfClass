namespace DemoApi.Domain;

public class StatusLookup : ILookupCurrentStatus, ILookupDevelopers
{
    public async Task<StatusResponse> GetCurrentStatusAsync()
    {
        var response = new StatusResponse
        {
            CreatedAt = DateTime.Now,
            Message = "Awesome. Party on Wayne"
        };
        return response;
    }

    public async Task<DeveloperInfo> GetOnCallDeveloperAsync()
    {
        var response = new DeveloperInfo { Name = "Bob Smith", Email = "Bob@aol.com" };
        return response;
    }
}
