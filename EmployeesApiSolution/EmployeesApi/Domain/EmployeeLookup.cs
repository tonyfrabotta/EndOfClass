using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EmployeesApi.Domain;

public class EmployeeLookup : ILookupEmployees, IManageEmployees
{
    private readonly EmployeesMongoDbAdapter _adapter;

    public EmployeeLookup(EmployeesMongoDbAdapter adapter)
    {
        _adapter = adapter;
    }

    public async Task<EmployeeDocumentResponse> CreateEmployeeAsync(EmployeeCreateRequest request)
    {
        var employeeToAdd = new Employee
        {
            Department = request.Department,
            Name = new NameInformation { FirstName = request.FirstName, LastName = request.LastName },
            Salary = request.StartingSalary
        };

        // This is a "side effect producing call"
        await _adapter.GetEmployeeCollection().InsertOneAsync(employeeToAdd);

        var response = new EmployeeDocumentResponse
        {
            Id = employeeToAdd.Id.ToString(),
            Department = employeeToAdd.Department,
            Name = new EmployeeNameInformation
            {
                FirstName = employeeToAdd.Name.FirstName,
                LastName = employeeToAdd.Name.LastName
            }
        };
        return response;
        
    }

    public async Task FireAsync(string id)
    {
        var bId = ObjectId.Parse(id);
        var filter = Builders<Employee>.Filter.Where(e => e.Id == bId);
        var update = Builders<Employee>.Update.Set(e => e.Removed, true);

        await _adapter.GetEmployeeCollection().UpdateOneAsync(filter, update);
       
    }

    public async Task<List<EmployeeSummaryResponse>> GetAllEmployeeSummariesAsync()
    {
        var query =  _adapter.GetEmployeeCollection().AsQueryable()
            .Where(e => !e.Removed.HasValue || e.Removed.Value == false)
            .OrderBy(e => e.Name.LastName)
             .Select(e => new EmployeeSummaryResponse
             {
                 Id = e.Id.ToString(),
                 Name = new EmployeeNameInformation { FirstName = e.Name.FirstName, LastName = e.Name.LastName }
             });


        // a thing we add here later.
        var response = await query.ToListAsync();
        return response;
    }

    public async Task<EmployeeDocumentResponse> GetEmployeeByIdAsync(string id)
    {

        // if we get here, that sucker is an ObjectID
        var bId = ObjectId.Parse(id);

        var projection = Builders<Employee>.Projection.Expression(emp => new EmployeeDocumentResponse
        {
            Id = emp.Id.ToString(),
            Department = emp.Department,
            Name = new EmployeeNameInformation
            {
                FirstName = emp.Name.FirstName,
                LastName = emp.Name.LastName
            }
        });

        var response = await _adapter.GetEmployeeCollection()
            .Find(e => e.Id == bId && !e.Removed.HasValue || e.Removed!.Value == false)
            .Project(projection).SingleOrDefaultAsync();

        return response;
    }

    public async Task<bool> UpdateDepartmentAsync(string id, string department)
    {
        var bId = ObjectId.Parse(id);
        var filter = Builders<Employee>.Filter.Where(e => e.Id == bId); // TODO: Only update employees that haven't been removed.
        var update = Builders<Employee>.Update.Set(e => e.Department, department);

        var changes = await _adapter.GetEmployeeCollection().UpdateOneAsync(filter, update);

        //return changes.ModifiedCount == 1;
        return changes.MatchedCount == 1;
    }

    public async Task<bool> UpdateNameAsync(string id, EmployeeNameInformation name)
    {
        var updatedName = new NameInformation { FirstName = name.FirstName, LastName = name.LastName };

        var bId = ObjectId.Parse(id);
        var filter = Builders<Employee>.Filter.Where(e => e.Id == bId); // TODO: Only update employees that haven't been removed.
        var update = Builders<Employee>.Update.Set(e => e.Name, updatedName);

        var changed = await _adapter.GetEmployeeCollection().UpdateOneAsync(filter, update);

        return changed.MatchedCount == 1;

    }
}
