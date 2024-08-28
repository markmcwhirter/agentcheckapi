using FastEndpoints;

using FluentValidation;

namespace AgentCheckApi.Agency;


public class Validator : Validator<AddAgencyRequest>
{
    public Validator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("your name is required!")
            .MinimumLength(10).WithMessage("name is too short!")
            .MaximumLength(50).WithMessage("name is too long!");


    }
    private bool BeAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}