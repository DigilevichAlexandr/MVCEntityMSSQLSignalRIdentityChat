using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MVCEntityMSSQLSignalR.BLL.Services;
using System;
using System.Threading.Tasks;

namespace MVCEntityMSSQLSignalR.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IBotService _bot;
        /// <summary>
        /// ChatHub constructor
        /// </summary>
        /// <param name="bot">Bot object</param>
        public ChatHub(IBotService bot)
        {
            _bot = bot;
        }
        /// <summary>
        /// Sending message method
        /// </summary>
        /// <param name="messageText">User's message</param>
        /// <returns></returns>
        public async Task Send(string messageText)
        {
            try
            {
                var userName = Context.User.Identity.Name;
                var answer = await _bot.HandleMessage(messageText, userName);

                foreach (string message in answer)
                {
                    await this.Clients.All.SendAsync("Send", userName +": "+ message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                await this.Clients.All.SendAsync("Send", "error!!!");
            }
        }
    }
}
