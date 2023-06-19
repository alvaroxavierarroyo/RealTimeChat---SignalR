using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace WebChatServer
{
    public static class Negotiate
    {
        [FunctionName("negotiate")]
       public static SignalRConnectionInfo NegotiateV(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [SignalRConnectionInfo(HubName ="MyChatRoom")] SignalRConnectionInfo connectionInfo
            )
            
        {
            return connectionInfo;
        }
    }
}
