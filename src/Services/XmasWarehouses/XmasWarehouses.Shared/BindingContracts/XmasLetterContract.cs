namespace XmasWarehouses.Shared.BindingContracts;

public class XmasLetterContract
{
	public string XmasLetterNumber { get; set; } = string.Empty;

	public DateTime ReceivedOn { get; set; } = DateTime.MinValue;
	public string ChildEmail { get; set; } = string.Empty;
	public string LetterSubject { get; set; } = string.Empty;
	public string LetterBody { get; set; } = string.Empty;

	public string LetterStatus { get; set; } = string.Empty;
}