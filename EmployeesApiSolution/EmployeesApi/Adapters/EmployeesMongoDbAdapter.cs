using EmployeesApi.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace EmployeesApi.Adapters;

public class EmployeesMongoDbAdapter
{
    private readonly IMongoCollection<Employee> _employeeCollection;
    private readonly ILogger<EmployeesMongoDbAdapter> _logger;

    public EmployeesMongoDbAdapter(ILogger<EmployeesMongoDbAdapter> logger, IOptions<MongoConnectionOptions> options)
    {
               
        _logger = logger;

        var clientSettings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);
        //clientSettings.LinqProvider = MongoDB.Driver.Linq.LinqProvider.V3;
        if(options.Value.LogCommands)
        {
            clientSettings.ClusterConfigurator = db =>
            {
                db.Subscribe<CommandStartedEvent>(e =>
                {
                    _logger.LogInformation($"Running {e.CommandName} - the command looks like this {e.Command.ToJson()}");
                });
            };
        }

        var conn = new MongoClient(clientSettings);

        var db = conn.GetDatabase(options.Value.Database);

        _employeeCollection = db.GetCollection<Employee>(options.Value.Collection);
    }


    public IMongoCollection<Employee> GetEmployeeCollection() => _employeeCollection;


}
