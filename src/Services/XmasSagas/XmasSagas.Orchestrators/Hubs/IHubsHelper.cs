namespace XmasSagas.Orchestrators.Hubs;

public interface IHubsHelper
{
	Task TellChildrenThatClientIsConnected(string user, string message);
	Task TellChildrenThatClientIsDisconnected(string user, string message);

	Task TellChildrenThatXmasSagaWasStarted(string user, string message);
	Task TellChildrenThatXmasLetterWasApproved(string user, string message);
	Task TellChildrenThatXmasLetterWasProcessed(string user, string message);
	Task TellChildrenThatXmasSagaWasCompleted(string user, string message);
}