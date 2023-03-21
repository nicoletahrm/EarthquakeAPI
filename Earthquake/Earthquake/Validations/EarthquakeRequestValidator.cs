using Earthquake.API.Models.Requests;
using FluentValidation;

namespace Earthquake.API.Validations
{
    public class EarthquakeRequestValidator : AbstractValidator<EarthquakeRequest>
    {
        private readonly List<string> orderByList = new()
        {
            "time",
            "time-asc",
            "magnitude",
            "magnitude-asc"
        };

        public EarthquakeRequestValidator()
        {
            // date format = yyyy-mm-dd
            RuleFor(e => e.StartTime)
                .LessThan(DateTime.Now)
                .WithMessage("StartTime is not a valid date.");

            RuleFor(e => e.EndTime)
                .LessThan(DateTime.Now)
                .WithMessage("EndTime is not a valid date.");

            RuleFor(e => new { e.StartTime, e.EndTime })
               .Must(x => StartTimeLessThanEndTime(x.StartTime, x.EndTime))
               .WithMessage("StartTime > EndTime");

            RuleFor(e => e.MaxMagnitude)
                .LessThan(10)
                .GreaterThan(-1)
                .WithMessage("MaxMagnitude needs to be bigger than -1 and less than 10.");

            RuleFor(e => e.OrderBy)
                .Must(BeOrderedBy).WithMessage("OrderBy is not valid. Try time, time-asc, magnitute or magnitude-asc.");
        }

        public bool BeOrderedBy(string orderBy)
        {
            return orderByList.Contains(orderBy) || orderBy == null;
        }

        public bool StartTimeLessThanEndTime(DateTime? startTime, DateTime? endTime)
        {
            return startTime < endTime;
        }
    }
}
