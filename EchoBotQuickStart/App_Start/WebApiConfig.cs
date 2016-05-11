using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Microsoft.Skype.Bots;
using Microsoft.Skype.Bots.Events.Messaging;
using Microsoft.Skype.Bots.Interfaces;
using Unity.WebApi;

namespace EchoBotQuickStart
{
    public static class WebApiConfig
    {
        
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            var container = new UnityContainer();

            RegisterTypes(container);

            config.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void RegisterTypes(UnityContainer container)
        {
            MessagingBotServiceSettings settings = MessagingBotServiceSettings.LoadFromCloudConfiguration();
            IMessagingBotService botService = new BotService(settings);

            container
                .RegisterInstance(botService, new ContainerControlledLifetimeManager());

            botService.OnPersonalChatMessageReceivedAsync += BotService_OnPersonalChatMessageReceivedAsync;
                //async receivedEvent =>            
                //    await receivedEvent.Reply("Well " + receivedEvent.Content + " " + i, true).ConfigureAwait(false);

            botService.OnContactAddedAsync +=
                async contactAdded =>
                    await contactAdded.Reply($"Hello {contactAdded.FromDisplayName}!").ConfigureAwait(false);
        }

        private async static Task BotService_OnPersonalChatMessageReceivedAsync(MessageReceivedEvent receivedEvent) {

            string answer = "I do not understand the question";

            string message = receivedEvent.Content;
            if (message == "Hello") {
                answer = "Hello " + receivedEvent.From;
            }

            await receivedEvent.Reply(answer, true).ConfigureAwait(false);
        }
    }
}
