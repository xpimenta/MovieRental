using FluentValidation;
using MovieRental.Payment;

namespace MovieRental.Rental;

public class RentalValidation : AbstractValidator<Rental>
{
    public RentalValidation()
    {
        RuleFor(r => r.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");
        RuleFor(r=>r.DaysRented)
            .GreaterThan(0).WithMessage("DaysRented must be greater than 0");
        RuleFor(r => r.PaymentMethod)
            .NotEmpty().WithMessage("PaymentMethod is required")
            .IsEnumName(typeof(PaymentMethod), caseSensitive: false)
            .WithMessage("PaymentMethod must be a valid payment method");
    }
}