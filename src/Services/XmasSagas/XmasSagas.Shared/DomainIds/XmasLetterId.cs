using Muflone.Core;

namespace XmasSagas.Shared.DomainIds;

public sealed class XmasLetterId : DomainId
{
	public XmasLetterId(Guid value) : base(value)
	{
	}
}