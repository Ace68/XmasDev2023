﻿namespace XmasBlazor.Shared.Enums;

public sealed class ModuleNames : Enumeration
{
	public static ModuleNames Production = new(1, "PROD", "Production");
	public static ModuleNames Pubs = new(1, "PUBS", "Pubs");

	public static IEnumerable<ModuleNames> List() => new[]
	{
		Production,
		Pubs
	};

	public ModuleNames(int id, string code, string name) : base(id, code, name)
	{
	}

	public static ModuleNames FromName(string name)
	{
		var element = List().SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

		if (element == null)
			throw new Exception($"Possible values for ModuleNames: {string.Join(",", List().Select(s => s.Name))}");

		return element;
	}

	public static ModuleNames FromCode(string code)
	{
		var element = List().SingleOrDefault(s => string.Equals(s.Code, code, StringComparison.CurrentCultureIgnoreCase));

		if (element == null)
			throw new Exception($"Possible values for ModuleNames: {string.Join(",", List().Select(s => s.Code))}");

		return element;
	}

	public static ModuleNames From(int id)
	{
		var element = List().SingleOrDefault(s => s.Id == id);

		if (element == null)
			throw new Exception($"Possible values for ModuleNames: {string.Join(",", List().Select(s => s.Name))}");

		return element;
	}
}