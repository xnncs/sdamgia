using Api.Contracts;
using Api.Contracts.Requests;
using FluentValidation;

namespace Api.Validation.Validators;

public class LoginUserRequestValidator : AbstractValidator<LoginUserRequest>
{
    public LoginUserRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty().Length(6, 30);
    }
}