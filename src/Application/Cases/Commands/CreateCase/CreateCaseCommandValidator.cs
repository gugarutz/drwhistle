using FluentValidation;

namespace DrWhistle.Application.Cases.Commands.CreateCase
{
    public class CreateCaseCommandValidator : AbstractValidator<CreateCaseCommand>
    {
        public CreateCaseCommandValidator()
        {
            RuleFor(c => c.caseDto.Title)
                .MaximumLength(150)
                .NotEmpty();
        }
    }
}
