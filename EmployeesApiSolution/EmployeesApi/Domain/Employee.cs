using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeesApi.Domain;

public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id
    {
        get; set;
    }

    [BsonElement("department")]
    public string Department
    {
        get; set;
    } = "";

    [BsonElement("name")]
    public NameInformation Name
    {
        get; set;
    } = new();

    [BsonElement("salary")]
    public decimal? Salary
    {
        get; set;
    }

    [BsonElement("removed")]
    public bool? Removed
    {
        get; set;
    } = false;

}


public class NameInformation
{
    [BsonElement("firstName")]
    public string FirstName
    {
        get; set;
    } = "";

    [BsonElement("lastName")]
    public string LastName
    {
        get; set;
    } = "";
}
