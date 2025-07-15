using Ardalis.SmartEnum;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GradTest.Utils;

public class SmartEnumSchemaFilter<TSmartEnum> : ISchemaFilter where TSmartEnum : SmartEnum<TSmartEnum>
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.MemberInfo?.Name != "CategoryValue") return;

        var options = SmartEnum<TSmartEnum>.List.Select(e => $"{e.Value} = {e.Name}");

        schema.Description += $" Allowed values: {string.Join(", ", options)}.";
    }
}



