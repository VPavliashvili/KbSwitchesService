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

            SetupComponent(swaggerDoc);
            SetupPath(swaggerDoc);
        }

        private void SetupComponent(OpenApiDocument swaggerDoc)
        {
            OpenApiSchema schema = new();

            schema.Type = "object";

            schema.Properties.Add("manufacturer", new()
            {
                Type = "string",
                Format = "string"
            });
            schema.Properties.Add("fullName", new()
            {
                Type = "string",
                Format = "string"
            });
            schema.Properties.Add("switchType", new()
            {
                Type = "string",
                Format = "string"
            });
            schema.Properties.Add("actuationForce", new()
            {
                Type = "integer",
                Format = "int32"
            });
            schema.Properties.Add("bottomoutForce", new()
            {
                Type = "integer",
                Format = "int32"
            });
            schema.Properties.Add("actuationDistance", new()
            {
                Type = "number",
                Format = "double"
            });
            schema.Properties.Add("bottomoutDistance", new()
            {
                Type = "number",
                Format = "double"
            });
            schema.Properties.Add("lifespan", new()
            {
                Type = "integer",
                Format = "int32"
            });

            swaggerDoc.Components.Schemas.Add("Switches", schema);
        }

        private void SetupPath(OpenApiDocument swaggerDoc)
        {
            swaggerDoc.Paths.Remove("/api/Switches");

            OpenApiPathItem apiSwitchesPathitem = new();
            OpenApiOperation apiSwitchesGetOperation = new();

            apiSwitchesGetOperation.Tags.Add(new OpenApiTag() { Name = "Switches" });

            OpenApiResponse response = new();
            response.Description = "Success";

            OpenApiMediaType plainTextMedia = new();

            OpenApiSchema schema = new();
            schema.Type = "array";
            schema.Items = new()
            {
                Reference = new()
                {
                    ExternalResource = "#/components/schemas/Switches"
                }
            };

            plainTextMedia.Schema = schema;

            response.Content.Add("text/plain", plainTextMedia);

            apiSwitchesGetOperation.Responses.Add("200", response);
            apiSwitchesPathitem.AddOperation(OperationType.Get, apiSwitchesGetOperation);

            swaggerDoc.Paths.Add("/api/Switches", apiSwitchesPathitem);
        }

    }

}
