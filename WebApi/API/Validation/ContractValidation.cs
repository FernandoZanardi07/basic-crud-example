using ExampleCrud.WebApi.API.DTOs;
using FluentValidation;

namespace ExampleCrud.WebApi.API.Validators;

public class ContactValidation : AbstractValidator<Contact>
{
    public ContactValidation()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("The Number field is required.")
            .MaximumLength(20).WithMessage("The Number field must have a maximum of 20 characters.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("The Type field is invalid.");
    }
}