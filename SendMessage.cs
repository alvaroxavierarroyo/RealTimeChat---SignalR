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
    public static class SendMessage
    {
        [FunctionName("SendMessage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous,"post", Route = "sendMessage")] HttpRequest req,
            ILogger log, [SignalR(HubName = "MyChatRoom")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            var receivedContent = await new StreamReader(req.Body).ReadToEndAsync();
            Message m = JsonConvert.DeserializeObject<Message>(receivedContent);
            await signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "newMessage",
                    Arguments = new[] {m} 
                });
               




            return new OkResult();
        }
    }
}

internal class Message
{
    public string text { get; set; }
}
