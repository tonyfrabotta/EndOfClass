namespace EmployeesApi;

public class MongoConnectionOptions
{
    public static string SectionName = "MongoDbConnection";

    public string ConnectionString
    {
        get; set;
    } = "";

    public string Database
    {
        get; set;
    } = "";

    public string Collection
    {
        get; set;
    } = "";
    public bool LogCommands
    {
        get; set;
    }
}
