using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shared.API.CustomFilters;

public class CustomOperationFilters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var ignoredProperties = context.MethodInfo.GetParameters()
            .SelectMany(p => p.ParameterType.GetProperties()
                .Where(prop => prop.GetCustomAttribute<JsonIgnoreAttribute>() != null)
            );
        var propertyInfos = ignoredProperties.ToList();
        if (!propertyInfos.Any()) return;
        {
            foreach (var property in propertyInfos)
            {
                operation.Parameters = operation.Parameters
                    .Where(p => !p.Name.Equals(property.Name, StringComparison.InvariantCulture))
                    .ToList();
            }
        }
    }
}