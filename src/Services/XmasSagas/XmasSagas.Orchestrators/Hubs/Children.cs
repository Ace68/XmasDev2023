namespace XmasSagas.Orchestrators.Hubs;

public static class ChildrenService
{
	public static List<Children> Childrens { get; } = new();
}

public record Children(string ConnectionId);