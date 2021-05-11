using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiProject.SwaggerFilters
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            if (swaggerDoc == null)
            {
                throw new ArgumentNullException(nameof(swaggerDoc));
            }


            swaggerDoc.Tags = new List<OpenApiTag> {
                    new OpenApiTag{ Name = "Switches", Description = string.Empty }
                };

            // swaggerDoc.Paths = swaggerDoc.Paths.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value) as OpenApiPaths;

        }
    }
}