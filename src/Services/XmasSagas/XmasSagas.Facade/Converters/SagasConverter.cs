using XmasSagas.Messages.Commands;
using XmasSagas.Shared.BindingContracts;
using XmasSagas.Shared.CustomTypes;
using XmasSagas.Shared.DomainIds;

namespace XmasSagas.Facade.Converters;

public static class SagasConverter
{
	internal static StartXmasLetterSaga ToCommand(this XmasLetterContract contract)
	{
		return new(new XmasLetterId(Guid.NewGuid()), Guid.NewGuid(), new XmasLetterNumber(contract.XmasLetterNumber),
			new ReceivedOn(contract.ReceivedOn), new ChildEmail(contract.ChildEmail),
			new LetterSubject(contract.LetterSubject), new LetterBody(contract.LetterBody));
	}
}