using FluentValidation;

namespace MovieRental.Movie;

public class MovieValidation : AbstractValidator<Movie>
{
    public MovieValidation()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .Length(2, 100).WithMessage("Title must be between 2 and 100 characters");
    }
}