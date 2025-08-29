using PersonRegistry.Api.Middlewares.Exceptions.Middlewares;

namespace PersonRegistry.Api.Middlewares.Exceptions;

public static class ExceptionMiddlewareExtensions
{
    public static IServiceCollection AddProblemDetailsMiddleware(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<FluentValidationExceptionMiddleware>();
        services.AddExceptionHandler<ApiExceptionMiddleware>();
        
        return services;
    }

    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();

        return app;
    }
}