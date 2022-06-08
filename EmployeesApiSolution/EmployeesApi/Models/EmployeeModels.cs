using System.ComponentModel.DataAnnotations;

namespace EmployeesApi.Models;

public record CollectionResponse<T>
{
    public List<T> Data
    {
        get; set;
    } = new();
}

public record EmployeeSummaryResponse
{
    public EmployeeNameInformation Name
    {
        get; set;
    } = new();
    public string Id
    {
        get; set;
    } = "";
}
public record EmployeeDocumentResponse
{
    public string Id
    {
        get; set;
    } = string.Empty;

    public EmployeeNameInformation Name
    {
        get; set;
    } = new();

    public string Department
    {
        get; init;
    } = string.Empty;
}

public record EmployeeNameInformation
{
    [Required]
    public string FirstName
    {
        get; init;
    } = string.Empty;

    [Required]
    public string LastName
    {
        get; init;
    } = string.Empty;
}



public class EmployeeCreateRequest : IValidatableObject
{
    [Required]
    [MaxLength(255)]
    [MinLength(1)]
    public string FirstName
    {
        get; set;
    } = string.Empty;
    [Required]
    public string LastName
    {
        get; set;
    } = string.Empty;

    [Required]
    public string Department
    {
        get; set;
    } = string.Empty;

    [Required]
    public decimal? StartingSalary
    {
        get; set;
    }

    [Required]
    public string FavoriteColor
    {
        get; set;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FirstName.Trim().ToUpper() == "DARTH" && LastName.Trim().ToUpper() == "VADER")
        {
            yield return new ValidationResult("We have a strict no Sith policy");
        }

        if (Department.Trim().ToUpper() == "DEV" && StartingSalary < 300000)
        {
            yield return new ValidationResult("Remember who writes your code.",
                new string[] { nameof(Department), nameof(StartingSalary) });
        }
    }
}
