namespace DemoApi.Domain;

public interface ILookupDevelopers
{
    Task<DeveloperInfo> GetOnCallDeveloperAsync();
}
