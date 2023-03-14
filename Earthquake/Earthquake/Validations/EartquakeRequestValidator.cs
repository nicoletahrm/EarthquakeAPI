using Earthquake.API.Models.Requests;
using FluentValidation;

namespace Earthquake.API.Validations
{
    public class EartquakeRequestValidator : AbstractValidator<EarthquakeRequest>
    {
        public EartquakeRequestValidator()
        {
            RuleFor(e => e.StartTime).NotNull().NotEmpty();
            RuleFor(e => e.EndTime).NotNull().WithMessage("EndDate is not a valid date.");

        }
    }
}
