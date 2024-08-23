using System.Text.Json;
using System.Text.Json.Serialization;

namespace API.Middlewares;

public class CustomErrorMiddleware
{
    private readonly RequestDelegate _next;

    public CustomErrorMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;

        using var newBodyStream = new MemoryStream();
        context.Response.Body = newBodyStream;

        try
        {
            await _next(context);
        }
        finally
        {
            context.Response.Body = originalBodyStream;
            newBodyStream.Seek(0, SeekOrigin.Begin);
            
            if (context.Response.StatusCode == StatusCodes.Status400BadRequest)
            {
                var responseBody = await new StreamReader(newBodyStream).ReadToEndAsync();
                await HandleBadRequestAsync(context, responseBody);
            }
            else
            {
                newBodyStream.Seek(0, SeekOrigin.Begin);
                await newBodyStream.CopyToAsync(originalBodyStream);
            }
        }
    }

    private static async Task HandleBadRequestAsync(HttpContext context, string responseBody)
    {
        var responseJson = JsonDocument.Parse(responseBody);
        var details = ExtractValidationErrors(responseJson);

        var errorResponse = new
        {
            error = new
            {
                status = StatusCodes.Status422UnprocessableEntity,
                code = "ValidationError",
                message = "Validation failed",
                details
            }
        };

        var errorJson = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

        await context.Response.WriteAsync(errorJson);
    }

    private static Dictionary<string, List<string>> ExtractValidationErrors(JsonDocument responseJson)
    {
        var errors = responseJson.RootElement.GetProperty("errors").EnumerateObject();
        var details = new Dictionary<string, List<string>>();

        foreach (var error in errors)
        {
            details[error.Name] = error.Value.EnumerateArray().Select(x => x.GetString()).ToList();
        }

        return details;
    }
}
