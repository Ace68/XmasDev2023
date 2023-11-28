namespace XmasSagas.Orchestrators.Hubs;

public interface IHubsHelper
{
	Task TellChildrenThatXmasLetterWasApproved(string message);
	Task TellChildrenThatXmasLetterWasProcessed(string message);
	Task TellChildrenThatXmasSagaWasCompleted(string message);
}