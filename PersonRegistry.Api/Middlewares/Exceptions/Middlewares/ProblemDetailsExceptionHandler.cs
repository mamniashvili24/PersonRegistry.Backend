using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PersonRegistry.Api.Middlewares.Exceptions.Middlewares;

public abstract class ProblemDetailsExceptionHandler<TException>
    : IExceptionHandler where TException : Exception
{
    private readonly IProblemDetailsService problemDetailsService;

    protected ProblemDetailsExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        this.problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not TException typedException)
            return false;

        ProblemDetails problemDetails = CreateProblemDetails(httpContext, typedException);

        await this.problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = typedException,
                ProblemDetails = problemDetails
            });

        return true;
    }

    protected abstract ProblemDetails CreateProblemDetails(HttpContext context, TException exception);
}