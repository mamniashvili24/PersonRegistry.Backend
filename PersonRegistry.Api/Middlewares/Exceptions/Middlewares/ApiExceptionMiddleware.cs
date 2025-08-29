using Microsoft.AspNetCore.Mvc;
using PersonRegistry.Domain.Exceptions;

namespace PersonRegistry.Api.Middlewares.Exceptions.Middlewares;

public class ApiExceptionMiddleware : ProblemDetailsExceptionHandler<ApiException>
{
    public ApiExceptionMiddleware(IProblemDetailsService problemDetailsService)
        : base(problemDetailsService)
    
    {
    }

    protected override ProblemDetails CreateProblemDetails(HttpContext context, ApiException exception)
    {
        return new ProblemDetails
        {
            Title = "Application Error",
            Status = StatusCodes.Status400BadRequest,
            Detail = exception.Message,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Instance = context.Request.Path
        };
    }
}