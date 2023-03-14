using Earthquake.API.Models.Requests;
using FluentValidation;

namespace Earthquake.API.Validations
{
    public class EartquakeRequestValidator : AbstractValidator<EarthquakeRequest>
    {
        private readonly List<string> orderByList = new()
        {
            "time",
            "time-asc",
            "magnitude",
            "magnitude-asc"
        };

        public EartquakeRequestValidator()
        {
            // date format = yyyy-mm-dd
            RuleFor(e => e.StartTime)
                .LessThan(DateTime.Now)
                .WithMessage("StartTime is not a valid date.");

            RuleFor(e => e.EndTime)
                .LessThan(DateTime.Now)
                .WithMessage("EndTime is not a valid date.");

            RuleFor(e => e.MaxMagnitude)
                .NotEmpty()
                .LessThan(10)
                .WithMessage("MaxMagnitude is too big. Needs to be less than 10.");

            RuleFor(e => e.OrderBy)
                .NotNull().NotEmpty()
                .Must(BeOrderedBy).WithMessage("OrderBy is not valid. Try time, time-asc, magnitute or magnitude-asc.");

            RuleFor(e => new {e.StartTime, e.EndTime})
                .NotNull().NotEmpty()
                .Must(x => StartTimeLessThanEndTime(x.StartTime, x.EndTime))
                .WithMessage("StartTime > EndTime");
        }

        public bool BeOrderedBy(string orderBy)
        {
            return orderByList.Contains(orderBy);
        }

        public bool StartTimeLessThanEndTime(DateTime? startTime, DateTime? endTime)
        {
            return startTime < endTime;
        }
    }
}
