using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace BrewUpNegotiate
{
	public class BrewUpAccess
	{
		[Function("negotiate")]
		public static HttpResponseData Negotiate([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequestData req,
			[SignalRConnectionInfoInput(HubName = "brewup")] string connectionInfo)
		{
			var response = req.CreateResponse(HttpStatusCode.OK);
			response.Headers.Add("Content-Type", "application/json");
			response.WriteString(connectionInfo);
			return response;
		}

		[Function("index")]
		public static HttpResponseData GetHomePage([HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequestData req)
		{
			var response = req.CreateResponse(HttpStatusCode.OK);
			response.WriteString(File.ReadAllText("content/index.html"));
			response.Headers.Add("Content-Type", "text/html");
			return response;
		}
	}
}