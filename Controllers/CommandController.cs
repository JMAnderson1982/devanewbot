using devanewbot.Services;
using Microsoft.AspNetCore.Mvc;
using SlackDotNet.Webhooks;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace devanewbot.Controllers
{
    public class CommandController : ControllerBase
    {
        private readonly SpongebobCommand SpongebobCommand;
        private readonly GifCommand GifCommand;

        public CommandController(SpongebobCommand spongebobCommand, GifCommand gifCommand)
        {
            SpongebobCommand = spongebobCommand;
            GifCommand = gifCommand;
        }

        [HttpPost]
        public async Task<OkResult> Spongebob(WebhookMessage webhookMessage)
        {
            await SpongebobCommand.ExecuteAsync(webhookMessage);
            return Ok();
        }

        [HttpPost]
        public async Task<OkResult> Gif(WebhookMessage webhookMessage)
        {
            await GifCommand.ExecuteAsync(webhookMessage);
            return Ok();
        }

        [HttpPost]
        public async Task<OkResult> Interactive(string payload)
        {
            var interactiveMessage = JsonConvert.DeserializeObject<InteractiveMessage>(payload);
            if (interactiveMessage.CallbackId == "gif")
            {
                await GifCommand.HandleInteractive(interactiveMessage);
            }

            return Ok();
        }
    }
}