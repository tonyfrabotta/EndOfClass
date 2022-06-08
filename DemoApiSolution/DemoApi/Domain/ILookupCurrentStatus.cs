namespace DemoApi.Domain;

public interface ILookupCurrentStatus
{
    Task<StatusResponse> GetCurrentStatusAsync();
}