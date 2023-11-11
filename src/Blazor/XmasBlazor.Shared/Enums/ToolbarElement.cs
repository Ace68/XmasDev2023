namespace XmasBlazor.Shared.Enums;

public class ToolbarElement : Enumeration
{
	public static ToolbarElement Add = new(1, "Add", "Add");
	public static ToolbarElement Edit = new(2, "Edit", "Edit");
	public static ToolbarElement Save = new(3, "Save", "Save");
	public static ToolbarElement ClearAll = new(5, "ClearAll", "ClearAll");
	public static ToolbarElement Delete = new(6, "Delete", "Delete");
	public static ToolbarElement Back = new(7, "Bck", "Back");
	public static ToolbarElement Close = new(8, "Close", "Close");

	public static IEnumerable<ToolbarElement> List() => new[] { Add, Edit, Save, ClearAll, Delete, Back, Close };

	public ToolbarElement(int id, string code, string name) : base(id, code, name)
	{
	}

	public static ToolbarElement FromName(string name)
	{
		var element = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

		if (element == null)
			throw new Exception($"Possible values for ToolbarElement: {string.Join(",", List().Select(s => s.Name))}");

		return element;
	}

	public static ToolbarElement FromCode(string code)
	{
		var element = List().SingleOrDefault(s => string.Equals(s.Code, code, StringComparison.CurrentCultureIgnoreCase));

		if (element == null)
			throw new Exception($"Possible values for ToolbarElement: {string.Join(",", List().Select(s => s.Code))}");

		return element;
	}

	public static ToolbarElement From(int id)
	{
		var element = List().SingleOrDefault(s => s.Id == id);

		if (element == null)
			throw new Exception($"Possible values for ToolbarElement: {string.Join(",", List().Select(s => s.Name))}");

		return element;
	}
}