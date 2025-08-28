using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace PersonRegistry.Application.Commons.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }
    
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!this.validators.Any())
            return await next(cancellationToken);

        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
        
        IEnumerable<Task<ValidationResult>> validators = this.validators.Select(v => v.ValidateAsync(context, cancellationToken));
        ValidationResult[] results = await Task.WhenAll(validators);
        
        List<ValidationFailure> failures = results.SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count != 0)
            throw new ValidationException(failures);

        return await next(cancellationToken);
    }
}