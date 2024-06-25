using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace Api.Validation.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

        IEnumerable<ValidationFailure> failures = _validators
            .Select(x => x.Validate(context))
            .SelectMany(results => results.Errors)
            .Where(failure => failure != null)
            .ToList();

        if (!failures.Any())
        {
            throw new ValidationException(failures);
        }

        return next();
    }
}