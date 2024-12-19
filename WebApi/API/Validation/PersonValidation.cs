using ExampleCrud.WebApi.API.DTOs;
using ExampleCrud.WebApi.Domain.Utilities;
using FluentValidation;

namespace ExampleCrud.WebApi.API.Validators;

public class PersonValidation : AbstractValidator<Person>
{
    public PersonValidation()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.CpfNumber).Must(Utility.IsCpfValid).WithMessage("Invalid CPF number.");
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");
        RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive is required.");

        When(x => x.Contacts != null && x.Contacts.Any(), () =>
        {
            RuleForEach(x => x.Contacts).SetValidator(new ContactValidation());
        });
    }
}
