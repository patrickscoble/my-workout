using FluentValidation;
using MyWorkout.Api.Resources;

namespace MyWorkout.Api.Validators
{
    public class SaveWorkoutExerciseResourceValidator : AbstractValidator<SaveWorkoutExerciseResource>
    {
        public SaveWorkoutExerciseResourceValidator()
        {
            RuleFor(we => we.WorkoutId)
                .NotEmpty()
                .WithMessage("'Workout Id' must not be 0.");

            RuleFor(we => we.ProgramExerciseId)
                .NotEmpty()
                .WithMessage("'Program Exercise Id' must not be 0.");

            RuleFor(we => we.Weight)
                .NotNull();

            RuleFor(we => we.MaxedOut)
                .NotNull();
        }
    }
}
