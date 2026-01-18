using FluentValidation;

namespace MovieRental.Customer;

public class CustomerValidation : AbstractValidator<Customer>
{
    public CustomerValidation()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters");
    }
}