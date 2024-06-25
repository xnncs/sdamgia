using Api.Contracts;
using Api.Contracts.Requests;
using FluentValidation;

namespace Api.Validation.Validators;

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator() 
    {
        RuleFor(x => x.Username).NotEmpty().Length(8, 20);

        RuleFor(x => x.FirstName).NotEmpty().Length(2, 35);
        RuleFor(x => x.LastName).Length(4, 35);

        RuleFor(x => x.Password).NotEmpty().Length(6, 30);
        RuleFor(x => x.Email).EmailAddress();

        RuleFor(x => x.RoleStudentOrTeacher).Must(x => x is >= 0 and <= 1);
    }
}