using FluentValidation;
using MyWorkout.Api.Resources;

namespace MyWorkout.Api.Validators
{
    public class SaveProgramResourceValidator : AbstractValidator<SaveProgramResource>
    {
        public SaveProgramResourceValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
