using FluentValidation;
using XmasReceiver.Shared.BindingContracts;

namespace XmasReceiver.Facade.Validators;

public class XmasLetterContractValidator : AbstractValidator<XmasLetterContract>
{
	public XmasLetterContractValidator()
	{
		RuleFor(v => v.XmasLetterNumber).NotEmpty();
		RuleFor(v => v.ChildEmail).NotEmpty();
		RuleFor(v => v.LetterBody).NotEmpty();
		RuleFor(v => v.ReceivedOn).GreaterThan(DateTime.MinValue);
	}
}