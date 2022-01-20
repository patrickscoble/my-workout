using FluentValidation;
using MyWorkout.Api.Resources;

namespace MyWorkout.Api.Validators
{
    public class SaveProgramExerciseResourceValidator : AbstractValidator<SaveProgramExerciseResource>
    {
        public SaveProgramExerciseResourceValidator()
        {
            RuleFor(pe => pe.ProgramId)
                .NotEmpty()
                .WithMessage("'Program Id' must not be 0.");

            RuleFor(pe => pe.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(pe => pe.Sets)
                .NotEmpty()
                .WithMessage("'Sets' must not be 0.");

            RuleFor(pe => pe.Repetitions)
                .NotEmpty();

            RuleFor(pe => pe.RestPeriod)
                .NotEmpty();
        }
    }
}
