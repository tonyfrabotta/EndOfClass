namespace EmployeesApi.Domain;

public interface IManageEmployees
{
    Task<EmployeeDocumentResponse> CreateEmployeeAsync(EmployeeCreateRequest request);
    Task FireAsync(string id);
    Task<bool> UpdateDepartmentAsync(string id, string department);
    Task<bool> UpdateNameAsync(string id, EmployeeNameInformation name);
}
