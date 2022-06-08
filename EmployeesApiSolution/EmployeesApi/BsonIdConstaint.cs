using MongoDB.Bson;

namespace EmployeesApi;

// "bsonid"
public class BsonIdConstaint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
       if(values.TryGetValue(routeKey, out var routeValue))
        {
            var parameterValue = Convert.ToString(routeValue);
            if(ObjectId.TryParse(parameterValue, out var _))
            {
                return true;
            } else
            {
                return false;
            }
        } else
        {
            return false;
        }
    }
}
