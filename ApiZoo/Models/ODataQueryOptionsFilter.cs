using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace ApiZoo.Models
{
    /// <summary>
    /// Class for options of query with ODATA
    /// </summary>
    /// <seealso cref="IOperationFilter" />
    public class ODataQueryOptionsFilter : IOperationFilter
    {
        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var queryAttribute = context.MethodInfo.GetCustomAttributes(true)
                .Union(context.MethodInfo.DeclaringType.GetCustomAttributes(true))
                .OfType<EnableQueryAttribute>().FirstOrDefault();

            if (queryAttribute != null)
            {
                //operation.Parameters?.RemoveAt(0);
                if (queryAttribute.AllowedQueryOptions.HasFlag(AllowedQueryOptions.Select))
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "$select",
                        In = ParameterLocation.Query,
                        Description = "Selects which properties to include in the response.",
                        Schema = new OpenApiSchema { Type = "string" }
                    });
                }

                if (queryAttribute.AllowedQueryOptions.HasFlag(AllowedQueryOptions.Expand))
                {
                    operation.Parameters.Add(new OpenApiParameter
                    {
                        Name = "$expand",
                        In = ParameterLocation.Query,
                        Description = "Expands related entities inline.",
                        Schema = new OpenApiSchema { Type = "string" }
                    });
                }
            }
        }
    }
}
