using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace PersonRegistry.Api.Middlewares.Exceptions.Middlewares;

public class FluentValidationExceptionMiddleware : ProblemDetailsExceptionHandler<ValidationException>
{
    public FluentValidationExceptionMiddleware(IProblemDetailsService problemDetailsService)
        : base(problemDetailsService)
    {
    }

    protected override ProblemDetails CreateProblemDetails(HttpContext context, ValidationException exception)
    {
        Dictionary<string, string[]> errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).Distinct().ToArray()
            );

        ValidationProblemDetails problemDetails = new(errors)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Validation Failed",
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Instance = context.Request.Path
        };

        return problemDetails;
    }
}