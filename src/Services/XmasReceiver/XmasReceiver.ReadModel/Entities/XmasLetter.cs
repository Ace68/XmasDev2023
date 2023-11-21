using XmasReceiver.ReadModel.Abstracts;
using XmasReceiver.Shared.BindingContracts;
using XmasReceiver.Shared.CustomTypes;
using XmasReceiver.Shared.DomainIds;
using XmasReceiver.Shared.Enums;

namespace XmasReceiver.ReadModel.Entities;

public class XmasLetter : EntityBase
{
	public string XmasLetterNumber { get; private set; } = string.Empty;

	public DateTime ReceivedOn { get; private set; } = DateTime.MinValue;
	public string ChildEmail { get; private set; } = string.Empty;
	public string LetterSubject { get; private set; } = string.Empty;
	public string LetterBody { get; private set; } = string.Empty;

	public string LetterStatus { get; private set; } = string.Empty;

	protected XmasLetter()
	{
	}

	internal static XmasLetter CreateXmasLetter(XmasLetterId aggregateId, XmasLetterNumber xmasLetterNumber, ReceivedOn receivedOn,
		ChildEmail childEmail, LetterSubject letterSubject, LetterBody letterBody, XmasLetterStatus xmasLetterStatus) =>
		new(aggregateId.Value.ToString(), xmasLetterNumber.Value, receivedOn.Value, childEmail.Value,
			letterSubject.Value, letterBody.Value, xmasLetterStatus.Name);

	private XmasLetter(string xmasLetterId, string xmasLetterNumber, DateTime receivedOn, string childEmail,
		string letterSubject, string letterBody, string letterStatus)
	{
		Id = xmasLetterId;
		XmasLetterNumber = xmasLetterNumber;

		ReceivedOn = receivedOn;
		ChildEmail = childEmail;

		LetterSubject = letterSubject;
		LetterBody = letterBody;

		LetterStatus = letterStatus;
	}

	public XmasLetterContract ToJson() => new()
	{
		XmasLetterNumber = XmasLetterNumber,
		ReceivedOn = ReceivedOn,
		ChildEmail = ChildEmail,
		LetterSubject = LetterSubject,
		LetterBody = LetterBody,

		LetterStatus = LetterStatus
	};
}