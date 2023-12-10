namespace XmasSagas.Orchestrators.Hubs;

public interface IHubsHelper
{
	Task TellChildrenThatXmasSagaWasStarted(string message);
	Task TellChildrenThatXmasLetterWasApproved(string message);
	Task TellChildrenThatXmasLetterWasProcessed(string message);
	Task TellChildrenThatXmasSagaWasCompleted(string message);
}