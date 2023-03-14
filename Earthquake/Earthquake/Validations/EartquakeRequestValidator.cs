using Earthquake.API.Models.Requests;
using FluentValidation;

namespace Earthquake.API.Validations
{
    public class EartquakeRequestValidator : AbstractValidator<EarthquakeRequest>
    {
        public EartquakeRequestValidator()
        {
            // date format = yyyy-mm-dd
            RuleFor(e => e.StartTime)
                .NotNull().NotEmpty()
                .LessThan(DateTime.Now)
                .WithMessage("StartTime is not a valid date.");

            RuleFor(e => e.EndTime)
                .NotNull().NotEmpty()
                .LessThan(DateTime.Now)
                .WithMessage("EndTime is not a valid date.");

            RuleFor(e => e.MaxMagnitude)
                .NotEmpty()
                .LessThan(10)
                .WithMessage("MaxMagnitude is too big.");

            RuleFor(e => e.OrderBy)
                .NotNull().NotEmpty()
                .Must(BeOrderedBy).WithMessage("OrderBy is not valid.");
        }

        public bool BeOrderedBy(String orderBy)
        {
            return (orderBy.Equals("time") || orderBy.Equals("time-asc") || orderBy.Equals("magnitude") || orderBy.Equals("magnitude-asc"));
        }
    }
}
