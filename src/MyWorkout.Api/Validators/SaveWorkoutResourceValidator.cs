using FluentValidation;
using MyWorkout.Api.Resources;

namespace MyWorkout.Api.Validators
{
    public class SaveWorkoutResourceValidator : AbstractValidator<SaveWorkoutResource>
    {
        public SaveWorkoutResourceValidator()
        {
            RuleFor(w => w.ProgramId)
                .NotEmpty()
                .WithMessage("'Program Id' must not be 0.");

            RuleFor(w => w.Date)
                .NotEmpty();
        }
    }
}
