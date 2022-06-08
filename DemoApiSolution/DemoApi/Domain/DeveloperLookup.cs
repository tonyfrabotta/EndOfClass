namespace DemoApi.Domain;

public class DeveloperLookup : ILookupDevelopers
{
    private readonly DeveloperApiAdapter _adapter;

    public DeveloperLookup(DeveloperApiAdapter adapter)
    {
        _adapter = adapter;
    }

    public async Task<DeveloperInfo> GetOnCallDeveloperAsync()
    {
        var rawFromApi = await _adapter.GetOnCallDeveloperAsync();
        if(rawFromApi != null)
        {
            return new DeveloperInfo
            {
                Name = rawFromApi.name,
                Email = rawFromApi.email // Note. This is "Mapping". Wish there was an automatic way to do this? Come Back tomorrow!
            };
        } else
        {
            throw new ArgumentNullException(); // more on this in a bit.
        }

    }
}
