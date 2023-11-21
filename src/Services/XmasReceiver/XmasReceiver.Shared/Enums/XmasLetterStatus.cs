namespace XmasReceiver.Shared.Enums;

public sealed class XmasLetterStatus : Enumeration
{
	public static XmasLetterStatus Received = new(1, "Rx", "Received");
	public static XmasLetterStatus Validated = new(2, "AX", "Validated");
	public static XmasLetterStatus Processed = new(3, "PX", "Processed");

	public static IEnumerable<XmasLetterStatus> List() => new[]
	{
		Received,
		Validated,
		Processed
	};

	public XmasLetterStatus(int id, string code, string name) : base(id, code, name)
	{
	}

	public static XmasLetterStatus FromName(string name)
	{
		var element = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

		if (element == null)
			throw new Exception($"Possible values for XmasLetterStatus: {string.Join(",", List().Select(s => s.Name))}");

		return element;
	}

	public static XmasLetterStatus FromCode(string code)
	{
		var element = List().SingleOrDefault(s => string.Equals(s.Code, code, StringComparison.CurrentCultureIgnoreCase));

		if (element == null)
			throw new Exception($"Possible values for XmasLetterStatus: {string.Join(",", List().Select(s => s.Code))}");

		return element;
	}

	public static XmasLetterStatus From(int id)
	{
		var element = List().SingleOrDefault(s => s.Id == id);

		if (element == null)
			throw new Exception($"Possible values for ModuleNames: {string.Join(",", List().Select(s => s.Name))}");

		return element;
	}
}