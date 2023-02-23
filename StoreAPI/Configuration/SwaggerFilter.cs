using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace StoreAPI.Configuration;

public class SwaggerFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null) return;

        foreach (var operationParameter in operation.Parameters)
        {
            if (operationParameter is not { } parameter) continue;
            var description = context.ApiDescription.ParameterDescriptions.First(
                p => p.Name == parameter.Name
                );
            var routeInfo = description.RouteInfo;
            parameter.Description ??= description.ModelMetadata?.Description;

            if (routeInfo == null) continue;

            parameter.Required |= !routeInfo.IsOptional;
        }
    }
}