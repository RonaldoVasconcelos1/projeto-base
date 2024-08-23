using System.Collections;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BaseController: ControllerBase
{
    protected IActionResult HandleResult<T>(ErrorOr<T> result, HttpMethodType httpMethod)
    {
        return result.Match<IActionResult>(
            success => HandleSuccess(success, httpMethod),
            error => HandleError(error));
    }

    private IActionResult HandleSuccess<T>(T success, HttpMethodType httpMethod)
    {
        var response = new { data = success };

        return httpMethod switch
        {
            HttpMethodType.GET when success is IEnumerable => Ok(response),
            HttpMethodType.GET => Ok(response),
            HttpMethodType.POST => StatusCode(201),
            HttpMethodType.PUT or HttpMethodType.DELETE or HttpMethodType.PATCH => Accepted(response),
            _ => Ok(response)
        };
    }

    private IActionResult HandleError(List<Error> errors)
    {
        var firstError = errors.First();

        var errorResponse = new
        {
            error = new
            {
                status = firstError.Type switch
                {
                    ErrorType.Validation => 422,
                    ErrorType.NotFound => 404,
                    _ => 500
                },
                code = firstError.Code,
                message = firstError.Description,
                details = errors.Select(e => e.Description).ToArray()
            }
        };

        return StatusCode(errorResponse.error.status, errorResponse);
    }

    protected IActionResult HandleGetAll<T>(ErrorOr<IEnumerable<T>> result)
    {
        return result.Match<IActionResult>(
            success => Ok(new { data = success }),
            error => HandleError(error));
    }

    protected IActionResult HandleGetById<T>(ErrorOr<T> result)
    {
        return result.Match<IActionResult>(
            success => Ok(new { data = success }),
            error => HandleError(error));
    }
}


public enum HttpMethodType
{
    GET,
    POST,
    PUT,
    DELETE,
    PATCH
}
