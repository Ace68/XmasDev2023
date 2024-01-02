using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace BrewUpServerless
{
	public class BrewUpFunctions
	{
		protected BrewUpFunctions()
		{ }

		[FunctionName("negotiate")]
		public static SignalRConnectionInfo Negotiate(
			[HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
			[SignalRConnectionInfo(HubName = "xmas")] SignalRConnectionInfo connectionInfo)
		{
			return connectionInfo;
		}

		[FunctionName("index")]
		public static IActionResult GetHomePage([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req, ExecutionContext context)
		{
			var path = Path.Combine(context.FunctionAppDirectory, "content", "index.html");
			return new ContentResult
			{
				Content = File.ReadAllText(path),
				ContentType = "text/html",
			};
		}

		[FunctionName("TellChildrenThatXmasSagaWasStarted")]
		public static Task TellChildrenThatXmasSagaWasStarted(
			[RabbitMQTrigger("TellChildrenThatXmasSagaWasStarted", ConnectionStringSetting = "RabbitMQConnectionString")] string myQueueItem,
			[SignalR(HubName = "xmas", ConnectionStringSetting = "AzureSignalRConnectionString")] IAsyncCollector<SignalRMessage> signalRMessages,
			ILogger log, ExecutionContext context)
		{
			log.LogInformation($"TellChildrenThatXmasSagaWasStarted processed: {myQueueItem}");

			return signalRMessages.AddAsync(
				new SignalRMessage
				{
					Target = "TellChildrenThatXmasSagaWasStarted",
					Arguments = new[] { myQueueItem }
				});
		}
	}
}
